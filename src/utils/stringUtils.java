import java.util.ArrayList;
import java.util.List;

public class StringUtils {

    public static void splitBy(List<String> out, String in, String splitter, boolean keepSplitter) {
        int i = 0;
        int start = 0;

        while (i <= in.length() - splitter.length()) {
            if (in.substring(i, i + splitter.length()).equals(splitter)) {
                if (keepSplitter) {
                    i++; // Equivalent to nextGlyph
                }

                out.add(in.substring(start, i));
                
                if (!keepSplitter) {
                    i++; // Equivalent to nextGlyph
                }

                start = i;
            } else {
                i++; // Equivalent to nextGlyph
            }
        }

        if (in.length() != start) {
            out.add(in.substring(start));
        }
    }

    public static String pad(String in, int size, StringPadAlignment alignment, String pad, String leftCap, String rightCap) {
        int capWidth = leftCap.length() + rightCap.length();
        int inSize = in.length();
        int padSize = size - (inSize + capWidth);

        if (padSize < 0) {
            if (size - capWidth >= 1) {
                if (alignment == StringPadAlignment.RIGHT) {
                    return leftCap + "…" + in.substring(inSize - (size - capWidth - 1)) + rightCap;
                } else {
                    return leftCap + in.substring(0, size - capWidth - 1) + "…" + rightCap;
                }
            } else {
                return leftCap + rightCap;
            }
        } else if (padSize == 0) {
            return leftCap + in + rightCap;
        } else {
            StringBuilder padStr = new StringBuilder();

            for (int i = 0; i < padSize; i++) {
                padStr.append(pad);
            }

            if (alignment == StringPadAlignment.RIGHT) {
                return padStr.toString() + leftCap + in + rightCap;
            } else {
                return leftCap + in + rightCap + padStr.toString();
            }
        }
    }

    public static String tabsToSpaces(String in, int tabWidth) {
        int widthSinceNewline = 0;
        StringBuilder out = new StringBuilder();
        int bytePos = 0;
        int start = 0;

        while (bytePos < in.length()) {
            if (in.charAt(bytePos) == '\t') {
                out.append(in.substring(start, bytePos));
                StringBuilder spacer = new StringBuilder();
                int spaces = tabWidth - (widthSinceNewline % tabWidth);
                for (int i = 0; i < spaces; i++) {
                    spacer.append(" ");
                }
                out.append(spacer);
                widthSinceNewline += spaces;
                bytePos++; // Equivalent to nextGlyph
                start = bytePos;
            } else {
                widthSinceNewline++;
                if (in.charAt(bytePos) == '\n') {
                    widthSinceNewline = 0;
                }
                bytePos++; // Equivalent to nextGlyph
            }
        }

        out.append(in.substring(start));
        return out.toString();
    }

    public static int getGlyphPosOf(String in, String pattern) {
        int glyph = 0;
        int bytePos = 0;

        while (bytePos < in.length()) {
            if (in.substring(bytePos, Math.min(bytePos + pattern.length(), in.length())).equals(pattern)) {
                return glyph;
            }

            bytePos++; // Equivalent to nextGlyph
            glyph++;
        }

        return -1;
    }
}

enum StringPadAlignment {
    LEFT,
    RIGHT,
    CENTER
}
