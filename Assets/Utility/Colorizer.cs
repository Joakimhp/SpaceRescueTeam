using UnityEngine;

public static class Colorize {
	public static string AddColor ( Object obj, Color desiredColor ) {
		return string.Format ( "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", ( byte ) ( desiredColor.r * 255f ), ( byte ) ( desiredColor.g * 255f ), ( byte ) ( desiredColor.b * 255f ), obj.ToString () );
	}

	public static string AddColor ( string text, Color desiredColor ) {
		return string.Format ( "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", ( byte ) ( desiredColor.r * 255f ), ( byte ) ( desiredColor.g * 255f ), ( byte ) ( desiredColor.b * 255f ), text );
	}

	public static string AddColor <T>( T genericVariable, Color desiredColor ) {
		return string.Format ( "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", ( byte ) ( desiredColor.r * 255f ), ( byte ) ( desiredColor.g * 255f ), ( byte ) ( desiredColor.b * 255f ), genericVariable.ToString () );
	}
}