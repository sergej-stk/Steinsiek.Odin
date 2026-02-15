"""Generate an SVG file with "Steinsiek" rendered in Norse Bold as path outlines.

GitHub strips CSS and external fonts from SVGs, so the text must be
converted to SVG <path> elements using the actual font glyph outlines.

Requirements:
    pip install fonttools

Usage:
    python scripts/generate-text-svg.py
"""

import pathlib
from fontTools.ttLib import TTFont
from fontTools.pens.svgPathPen import SVGPathPen

FONT_PATH = (
    pathlib.Path(__file__).resolve().parents[1]
    / "src"
    / "Steinsiek.Odin.Web"
    / "Steinsiek.Odin.Web"
    / "wwwroot"
    / "assets"
    / "fonts"
    / "norse.bold.otf"
)
OUTPUT_PATH = (
    pathlib.Path(__file__).resolve().parents[1] / "assets" / "steinsiek-text.svg"
)

TEXT = "Steinsiek"
FILL_COLOR = "#212529"  # Bootstrap dark
LETTER_SPACING_EM = 0.15  # Matches .font-norse CSS class


def main():
    font = TTFont(str(FONT_PATH))
    glyph_set = font.getGlyphSet()
    cmap = font.getBestCmap()
    units_per_em = font["head"].unitsPerEm

    letter_spacing = units_per_em * LETTER_SPACING_EM

    # Collect glyph paths and compute total width
    glyphs = []
    cursor_x = 0.0

    for char in TEXT:
        glyph_name = cmap.get(ord(char))
        if glyph_name is None:
            raise ValueError(f"Character '{char}' not found in font")

        pen = SVGPathPen(glyph_set)
        glyph_set[glyph_name].draw(pen)
        path_data = pen.getCommands()
        advance_width = glyph_set[glyph_name].width

        glyphs.append({
            "char": char,
            "path": path_data,
            "x_offset": cursor_x,
        })
        cursor_x += advance_width + letter_spacing

    # Remove trailing letter-spacing
    cursor_x -= letter_spacing

    total_width = cursor_x

    # Get font vertical metrics for bounding box
    os2 = font["OS/2"]
    ascender = os2.sTypoAscender
    descender = os2.sTypoDescender
    total_height = ascender - descender

    # Build SVG with y-axis flip (font coords are y-up, SVG is y-down)
    # Transform: scale(1, -1) then translate(0, -ascender) to flip
    path_elements = []
    for glyph in glyphs:
        if glyph["path"]:
            path_elements.append(
                f'    <path d="{glyph["path"]}" '
                f'transform="translate({glyph["x_offset"]}, 0)"/>'
            )

    paths_joined = "\n".join(path_elements)

    svg = f"""<svg xmlns="http://www.w3.org/2000/svg"
     viewBox="0 0 {total_width} {total_height}"
     fill="{FILL_COLOR}"
     role="img"
     aria-label="Steinsiek">
  <g transform="scale(1, -1) translate(0, {-ascender})">
{paths_joined}
  </g>
</svg>
"""

    OUTPUT_PATH.parent.mkdir(parents=True, exist_ok=True)
    OUTPUT_PATH.write_text(svg, encoding="utf-8")
    print(f"Generated: {OUTPUT_PATH}")
    print(f"Dimensions: {total_width} x {total_height} units")
    print(f"Characters: {len(glyphs)}")


if __name__ == "__main__":
    main()
