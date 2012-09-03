using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Runtime.InteropServices;
using Ibasa.Numerics.Geometry;

namespace Ibasa.Numerics
{
	/// <summary>
	/// Represents a color.
	/// </summary>
	[Serializable]
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public struct Colorf: IEquatable<Colorf>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:255 Alpha:0.
		/// </summary>
		public static Colorf Transparent
		{
			get { return new Colorf(1f, 1f, 1f, 0f); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:248 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf AliceBlue
		{
			get { return new Colorf(0.941176470588235f, 0.972549019607843f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:235 Blue:215 Alpha:255.
		/// </summary>
		public static Colorf AntiqueWhite
		{
			get { return new Colorf(0.980392156862745f, 0.92156862745098f, 0.843137254901961f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf Aqua
		{
			get { return new Colorf(0f, 1f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:127 Green:255 Blue:212 Alpha:255.
		/// </summary>
		public static Colorf Aquamarine
		{
			get { return new Colorf(0.498039215686275f, 1f, 0.831372549019608f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf Azure
		{
			get { return new Colorf(0.941176470588235f, 1f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:245 Blue:220 Alpha:255.
		/// </summary>
		public static Colorf Beige
		{
			get { return new Colorf(0.96078431372549f, 0.96078431372549f, 0.862745098039216f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:228 Blue:196 Alpha:255.
		/// </summary>
		public static Colorf Bisque
		{
			get { return new Colorf(1f, 0.894117647058824f, 0.768627450980392f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Black
		{
			get { return new Colorf(0f, 0f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:235 Blue:205 Alpha:255.
		/// </summary>
		public static Colorf BlanchedAlmond
		{
			get { return new Colorf(1f, 0.92156862745098f, 0.803921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf Blue
		{
			get { return new Colorf(0f, 0f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:138 Green:43 Blue:226 Alpha:255.
		/// </summary>
		public static Colorf BlueViolet
		{
			get { return new Colorf(0.541176470588235f, 0.168627450980392f, 0.886274509803922f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:165 Green:42 Blue:42 Alpha:255.
		/// </summary>
		public static Colorf Brown
		{
			get { return new Colorf(0.647058823529412f, 0.164705882352941f, 0.164705882352941f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:222 Green:184 Blue:135 Alpha:255.
		/// </summary>
		public static Colorf BurlyWood
		{
			get { return new Colorf(0.870588235294118f, 0.72156862745098f, 0.529411764705882f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:95 Green:158 Blue:160 Alpha:255.
		/// </summary>
		public static Colorf CadetBlue
		{
			get { return new Colorf(0.372549019607843f, 0.619607843137255f, 0.627450980392157f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:127 Green:255 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Chartreuse
		{
			get { return new Colorf(0.498039215686275f, 1f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:210 Green:105 Blue:30 Alpha:255.
		/// </summary>
		public static Colorf Chocolate
		{
			get { return new Colorf(0.823529411764706f, 0.411764705882353f, 0.117647058823529f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:127 Blue:80 Alpha:255.
		/// </summary>
		public static Colorf Coral
		{
			get { return new Colorf(1f, 0.498039215686275f, 0.313725490196078f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:100 Green:149 Blue:237 Alpha:255.
		/// </summary>
		public static Colorf CornflowerBlue
		{
			get { return new Colorf(0.392156862745098f, 0.584313725490196f, 0.929411764705882f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:248 Blue:220 Alpha:255.
		/// </summary>
		public static Colorf Cornsilk
		{
			get { return new Colorf(1f, 0.972549019607843f, 0.862745098039216f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:220 Green:20 Blue:60 Alpha:255.
		/// </summary>
		public static Colorf Crimson
		{
			get { return new Colorf(0.862745098039216f, 0.0784313725490196f, 0.235294117647059f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf Cyan
		{
			get { return new Colorf(0f, 1f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:139 Alpha:255.
		/// </summary>
		public static Colorf DarkBlue
		{
			get { return new Colorf(0f, 0f, 0.545098039215686f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:139 Blue:139 Alpha:255.
		/// </summary>
		public static Colorf DarkCyan
		{
			get { return new Colorf(0f, 0.545098039215686f, 0.545098039215686f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:184 Green:134 Blue:11 Alpha:255.
		/// </summary>
		public static Colorf DarkGoldenrod
		{
			get { return new Colorf(0.72156862745098f, 0.525490196078431f, 0.0431372549019608f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:169 Green:169 Blue:169 Alpha:255.
		/// </summary>
		public static Colorf DarkGray
		{
			get { return new Colorf(0.662745098039216f, 0.662745098039216f, 0.662745098039216f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:100 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf DarkGreen
		{
			get { return new Colorf(0f, 0.392156862745098f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:189 Green:183 Blue:107 Alpha:255.
		/// </summary>
		public static Colorf DarkKhaki
		{
			get { return new Colorf(0.741176470588235f, 0.717647058823529f, 0.419607843137255f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:139 Green:0 Blue:139 Alpha:255.
		/// </summary>
		public static Colorf DarkMagenta
		{
			get { return new Colorf(0.545098039215686f, 0f, 0.545098039215686f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:85 Green:107 Blue:47 Alpha:255.
		/// </summary>
		public static Colorf DarkOliveGreen
		{
			get { return new Colorf(0.333333333333333f, 0.419607843137255f, 0.184313725490196f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:140 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf DarkOrange
		{
			get { return new Colorf(1f, 0.549019607843137f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:153 Green:50 Blue:204 Alpha:255.
		/// </summary>
		public static Colorf DarkOrchid
		{
			get { return new Colorf(0.6f, 0.196078431372549f, 0.8f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:139 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf DarkRed
		{
			get { return new Colorf(0.545098039215686f, 0f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:233 Green:150 Blue:122 Alpha:255.
		/// </summary>
		public static Colorf DarkSalmon
		{
			get { return new Colorf(0.913725490196078f, 0.588235294117647f, 0.47843137254902f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:143 Green:188 Blue:139 Alpha:255.
		/// </summary>
		public static Colorf DarkSeaGreen
		{
			get { return new Colorf(0.56078431372549f, 0.737254901960784f, 0.545098039215686f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:72 Green:61 Blue:139 Alpha:255.
		/// </summary>
		public static Colorf DarkSlateBlue
		{
			get { return new Colorf(0.282352941176471f, 0.23921568627451f, 0.545098039215686f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:47 Green:79 Blue:79 Alpha:255.
		/// </summary>
		public static Colorf DarkSlateGray
		{
			get { return new Colorf(0.184313725490196f, 0.309803921568627f, 0.309803921568627f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:206 Blue:209 Alpha:255.
		/// </summary>
		public static Colorf DarkTurquoise
		{
			get { return new Colorf(0f, 0.807843137254902f, 0.819607843137255f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:148 Green:0 Blue:211 Alpha:255.
		/// </summary>
		public static Colorf DarkViolet
		{
			get { return new Colorf(0.580392156862745f, 0f, 0.827450980392157f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:20 Blue:147 Alpha:255.
		/// </summary>
		public static Colorf DeepPink
		{
			get { return new Colorf(1f, 0.0784313725490196f, 0.576470588235294f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:191 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf DeepSkyBlue
		{
			get { return new Colorf(0f, 0.749019607843137f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:105 Green:105 Blue:105 Alpha:255.
		/// </summary>
		public static Colorf DimGray
		{
			get { return new Colorf(0.411764705882353f, 0.411764705882353f, 0.411764705882353f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:30 Green:144 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf DodgerBlue
		{
			get { return new Colorf(0.117647058823529f, 0.564705882352941f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:178 Green:34 Blue:34 Alpha:255.
		/// </summary>
		public static Colorf Firebrick
		{
			get { return new Colorf(0.698039215686274f, 0.133333333333333f, 0.133333333333333f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:250 Blue:240 Alpha:255.
		/// </summary>
		public static Colorf FloralWhite
		{
			get { return new Colorf(1f, 0.980392156862745f, 0.941176470588235f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:34 Green:139 Blue:34 Alpha:255.
		/// </summary>
		public static Colorf ForestGreen
		{
			get { return new Colorf(0.133333333333333f, 0.545098039215686f, 0.133333333333333f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:0 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf Fuchsia
		{
			get { return new Colorf(1f, 0f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:220 Green:220 Blue:220 Alpha:255.
		/// </summary>
		public static Colorf Gainsboro
		{
			get { return new Colorf(0.862745098039216f, 0.862745098039216f, 0.862745098039216f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:248 Green:248 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf GhostWhite
		{
			get { return new Colorf(0.972549019607843f, 0.972549019607843f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:215 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Gold
		{
			get { return new Colorf(1f, 0.843137254901961f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:218 Green:165 Blue:32 Alpha:255.
		/// </summary>
		public static Colorf Goldenrod
		{
			get { return new Colorf(0.854901960784314f, 0.647058823529412f, 0.125490196078431f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:128 Blue:128 Alpha:255.
		/// </summary>
		public static Colorf Gray
		{
			get { return new Colorf(0.501960784313725f, 0.501960784313725f, 0.501960784313725f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:128 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Green
		{
			get { return new Colorf(0f, 0.501960784313725f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:173 Green:255 Blue:47 Alpha:255.
		/// </summary>
		public static Colorf GreenYellow
		{
			get { return new Colorf(0.67843137254902f, 1f, 0.184313725490196f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:255 Blue:240 Alpha:255.
		/// </summary>
		public static Colorf Honeydew
		{
			get { return new Colorf(0.941176470588235f, 1f, 0.941176470588235f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:105 Blue:180 Alpha:255.
		/// </summary>
		public static Colorf HotPink
		{
			get { return new Colorf(1f, 0.411764705882353f, 0.705882352941177f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:205 Green:92 Blue:92 Alpha:255.
		/// </summary>
		public static Colorf IndianRed
		{
			get { return new Colorf(0.803921568627451f, 0.36078431372549f, 0.36078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:75 Green:0 Blue:130 Alpha:255.
		/// </summary>
		public static Colorf Indigo
		{
			get { return new Colorf(0.294117647058824f, 0f, 0.509803921568627f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:240 Alpha:255.
		/// </summary>
		public static Colorf Ivory
		{
			get { return new Colorf(1f, 1f, 0.941176470588235f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:230 Blue:140 Alpha:255.
		/// </summary>
		public static Colorf Khaki
		{
			get { return new Colorf(0.941176470588235f, 0.901960784313726f, 0.549019607843137f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:230 Green:230 Blue:250 Alpha:255.
		/// </summary>
		public static Colorf Lavender
		{
			get { return new Colorf(0.901960784313726f, 0.901960784313726f, 0.980392156862745f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:240 Blue:245 Alpha:255.
		/// </summary>
		public static Colorf LavenderBlush
		{
			get { return new Colorf(1f, 0.941176470588235f, 0.96078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:124 Green:252 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf LawnGreen
		{
			get { return new Colorf(0.486274509803922f, 0.988235294117647f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:250 Blue:205 Alpha:255.
		/// </summary>
		public static Colorf LemonChiffon
		{
			get { return new Colorf(1f, 0.980392156862745f, 0.803921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:173 Green:216 Blue:230 Alpha:255.
		/// </summary>
		public static Colorf LightBlue
		{
			get { return new Colorf(0.67843137254902f, 0.847058823529412f, 0.901960784313726f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:128 Blue:128 Alpha:255.
		/// </summary>
		public static Colorf LightCoral
		{
			get { return new Colorf(0.941176470588235f, 0.501960784313725f, 0.501960784313725f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:224 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf LightCyan
		{
			get { return new Colorf(0.87843137254902f, 1f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:250 Blue:210 Alpha:255.
		/// </summary>
		public static Colorf LightGoldenrodYellow
		{
			get { return new Colorf(0.980392156862745f, 0.980392156862745f, 0.823529411764706f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:144 Green:238 Blue:144 Alpha:255.
		/// </summary>
		public static Colorf LightGreen
		{
			get { return new Colorf(0.564705882352941f, 0.933333333333333f, 0.564705882352941f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:211 Green:211 Blue:211 Alpha:255.
		/// </summary>
		public static Colorf LightGray
		{
			get { return new Colorf(0.827450980392157f, 0.827450980392157f, 0.827450980392157f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:182 Blue:193 Alpha:255.
		/// </summary>
		public static Colorf LightPink
		{
			get { return new Colorf(1f, 0.713725490196078f, 0.756862745098039f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:160 Blue:122 Alpha:255.
		/// </summary>
		public static Colorf LightSalmon
		{
			get { return new Colorf(1f, 0.627450980392157f, 0.47843137254902f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:32 Green:178 Blue:170 Alpha:255.
		/// </summary>
		public static Colorf LightSeaGreen
		{
			get { return new Colorf(0.125490196078431f, 0.698039215686274f, 0.666666666666667f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:135 Green:206 Blue:250 Alpha:255.
		/// </summary>
		public static Colorf LightSkyBlue
		{
			get { return new Colorf(0.529411764705882f, 0.807843137254902f, 0.980392156862745f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:119 Green:136 Blue:153 Alpha:255.
		/// </summary>
		public static Colorf LightSlateGray
		{
			get { return new Colorf(0.466666666666667f, 0.533333333333333f, 0.6f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:176 Green:196 Blue:222 Alpha:255.
		/// </summary>
		public static Colorf LightSteelBlue
		{
			get { return new Colorf(0.690196078431373f, 0.768627450980392f, 0.870588235294118f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:224 Alpha:255.
		/// </summary>
		public static Colorf LightYellow
		{
			get { return new Colorf(1f, 1f, 0.87843137254902f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Lime
		{
			get { return new Colorf(0f, 1f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:50 Green:205 Blue:50 Alpha:255.
		/// </summary>
		public static Colorf LimeGreen
		{
			get { return new Colorf(0.196078431372549f, 0.803921568627451f, 0.196078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:240 Blue:230 Alpha:255.
		/// </summary>
		public static Colorf Linen
		{
			get { return new Colorf(0.980392156862745f, 0.941176470588235f, 0.901960784313726f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:0 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf Magenta
		{
			get { return new Colorf(1f, 0f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Maroon
		{
			get { return new Colorf(0.501960784313725f, 0f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:102 Green:205 Blue:170 Alpha:255.
		/// </summary>
		public static Colorf MediumAquamarine
		{
			get { return new Colorf(0.4f, 0.803921568627451f, 0.666666666666667f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:205 Alpha:255.
		/// </summary>
		public static Colorf MediumBlue
		{
			get { return new Colorf(0f, 0f, 0.803921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:186 Green:85 Blue:211 Alpha:255.
		/// </summary>
		public static Colorf MediumOrchid
		{
			get { return new Colorf(0.729411764705882f, 0.333333333333333f, 0.827450980392157f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:147 Green:112 Blue:219 Alpha:255.
		/// </summary>
		public static Colorf MediumPurple
		{
			get { return new Colorf(0.576470588235294f, 0.43921568627451f, 0.858823529411765f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:60 Green:179 Blue:113 Alpha:255.
		/// </summary>
		public static Colorf MediumSeaGreen
		{
			get { return new Colorf(0.235294117647059f, 0.701960784313725f, 0.443137254901961f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:123 Green:104 Blue:238 Alpha:255.
		/// </summary>
		public static Colorf MediumSlateBlue
		{
			get { return new Colorf(0.482352941176471f, 0.407843137254902f, 0.933333333333333f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:250 Blue:154 Alpha:255.
		/// </summary>
		public static Colorf MediumSpringGreen
		{
			get { return new Colorf(0f, 0.980392156862745f, 0.603921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:72 Green:209 Blue:204 Alpha:255.
		/// </summary>
		public static Colorf MediumTurquoise
		{
			get { return new Colorf(0.282352941176471f, 0.819607843137255f, 0.8f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:199 Green:21 Blue:133 Alpha:255.
		/// </summary>
		public static Colorf MediumVioletRed
		{
			get { return new Colorf(0.780392156862745f, 0.0823529411764706f, 0.52156862745098f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:25 Green:25 Blue:112 Alpha:255.
		/// </summary>
		public static Colorf MidnightBlue
		{
			get { return new Colorf(0.0980392156862745f, 0.0980392156862745f, 0.43921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:255 Blue:250 Alpha:255.
		/// </summary>
		public static Colorf MintCream
		{
			get { return new Colorf(0.96078431372549f, 1f, 0.980392156862745f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:228 Blue:225 Alpha:255.
		/// </summary>
		public static Colorf MistyRose
		{
			get { return new Colorf(1f, 0.894117647058824f, 0.882352941176471f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:228 Blue:181 Alpha:255.
		/// </summary>
		public static Colorf Moccasin
		{
			get { return new Colorf(1f, 0.894117647058824f, 0.709803921568627f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:222 Blue:173 Alpha:255.
		/// </summary>
		public static Colorf NavajoWhite
		{
			get { return new Colorf(1f, 0.870588235294118f, 0.67843137254902f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:128 Alpha:255.
		/// </summary>
		public static Colorf Navy
		{
			get { return new Colorf(0f, 0f, 0.501960784313725f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:253 Green:245 Blue:230 Alpha:255.
		/// </summary>
		public static Colorf OldLace
		{
			get { return new Colorf(0.992156862745098f, 0.96078431372549f, 0.901960784313726f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:128 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Olive
		{
			get { return new Colorf(0.501960784313725f, 0.501960784313725f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:107 Green:142 Blue:35 Alpha:255.
		/// </summary>
		public static Colorf OliveDrab
		{
			get { return new Colorf(0.419607843137255f, 0.556862745098039f, 0.137254901960784f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:165 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Orange
		{
			get { return new Colorf(1f, 0.647058823529412f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:69 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf OrangeRed
		{
			get { return new Colorf(1f, 0.270588235294118f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:218 Green:112 Blue:214 Alpha:255.
		/// </summary>
		public static Colorf Orchid
		{
			get { return new Colorf(0.854901960784314f, 0.43921568627451f, 0.83921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:238 Green:232 Blue:170 Alpha:255.
		/// </summary>
		public static Colorf PaleGoldenrod
		{
			get { return new Colorf(0.933333333333333f, 0.909803921568627f, 0.666666666666667f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:152 Green:251 Blue:152 Alpha:255.
		/// </summary>
		public static Colorf PaleGreen
		{
			get { return new Colorf(0.596078431372549f, 0.984313725490196f, 0.596078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:175 Green:238 Blue:238 Alpha:255.
		/// </summary>
		public static Colorf PaleTurquoise
		{
			get { return new Colorf(0.686274509803922f, 0.933333333333333f, 0.933333333333333f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:219 Green:112 Blue:147 Alpha:255.
		/// </summary>
		public static Colorf PaleVioletRed
		{
			get { return new Colorf(0.858823529411765f, 0.43921568627451f, 0.576470588235294f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:239 Blue:213 Alpha:255.
		/// </summary>
		public static Colorf PapayaWhip
		{
			get { return new Colorf(1f, 0.937254901960784f, 0.835294117647059f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:218 Blue:185 Alpha:255.
		/// </summary>
		public static Colorf PeachPuff
		{
			get { return new Colorf(1f, 0.854901960784314f, 0.725490196078431f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:205 Green:133 Blue:63 Alpha:255.
		/// </summary>
		public static Colorf Peru
		{
			get { return new Colorf(0.803921568627451f, 0.52156862745098f, 0.247058823529412f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:192 Blue:203 Alpha:255.
		/// </summary>
		public static Colorf Pink
		{
			get { return new Colorf(1f, 0.752941176470588f, 0.796078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:221 Green:160 Blue:221 Alpha:255.
		/// </summary>
		public static Colorf Plum
		{
			get { return new Colorf(0.866666666666667f, 0.627450980392157f, 0.866666666666667f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:176 Green:224 Blue:230 Alpha:255.
		/// </summary>
		public static Colorf PowderBlue
		{
			get { return new Colorf(0.690196078431373f, 0.87843137254902f, 0.901960784313726f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:0 Blue:128 Alpha:255.
		/// </summary>
		public static Colorf Purple
		{
			get { return new Colorf(0.501960784313725f, 0f, 0.501960784313725f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Red
		{
			get { return new Colorf(1f, 0f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:188 Green:143 Blue:143 Alpha:255.
		/// </summary>
		public static Colorf RosyBrown
		{
			get { return new Colorf(0.737254901960784f, 0.56078431372549f, 0.56078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:65 Green:105 Blue:225 Alpha:255.
		/// </summary>
		public static Colorf RoyalBlue
		{
			get { return new Colorf(0.254901960784314f, 0.411764705882353f, 0.882352941176471f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:139 Green:69 Blue:19 Alpha:255.
		/// </summary>
		public static Colorf SaddleBrown
		{
			get { return new Colorf(0.545098039215686f, 0.270588235294118f, 0.0745098039215686f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:128 Blue:114 Alpha:255.
		/// </summary>
		public static Colorf Salmon
		{
			get { return new Colorf(0.980392156862745f, 0.501960784313725f, 0.447058823529412f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:244 Green:164 Blue:96 Alpha:255.
		/// </summary>
		public static Colorf SandyBrown
		{
			get { return new Colorf(0.956862745098039f, 0.643137254901961f, 0.376470588235294f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:46 Green:139 Blue:87 Alpha:255.
		/// </summary>
		public static Colorf SeaGreen
		{
			get { return new Colorf(0.180392156862745f, 0.545098039215686f, 0.341176470588235f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:245 Blue:238 Alpha:255.
		/// </summary>
		public static Colorf SeaShell
		{
			get { return new Colorf(1f, 0.96078431372549f, 0.933333333333333f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:160 Green:82 Blue:45 Alpha:255.
		/// </summary>
		public static Colorf Sienna
		{
			get { return new Colorf(0.627450980392157f, 0.32156862745098f, 0.176470588235294f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:192 Green:192 Blue:192 Alpha:255.
		/// </summary>
		public static Colorf Silver
		{
			get { return new Colorf(0.752941176470588f, 0.752941176470588f, 0.752941176470588f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:135 Green:206 Blue:235 Alpha:255.
		/// </summary>
		public static Colorf SkyBlue
		{
			get { return new Colorf(0.529411764705882f, 0.807843137254902f, 0.92156862745098f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:106 Green:90 Blue:205 Alpha:255.
		/// </summary>
		public static Colorf SlateBlue
		{
			get { return new Colorf(0.415686274509804f, 0.352941176470588f, 0.803921568627451f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:112 Green:128 Blue:144 Alpha:255.
		/// </summary>
		public static Colorf SlateGray
		{
			get { return new Colorf(0.43921568627451f, 0.501960784313725f, 0.564705882352941f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:250 Blue:250 Alpha:255.
		/// </summary>
		public static Colorf Snow
		{
			get { return new Colorf(1f, 0.980392156862745f, 0.980392156862745f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:127 Alpha:255.
		/// </summary>
		public static Colorf SpringGreen
		{
			get { return new Colorf(0f, 1f, 0.498039215686275f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:70 Green:130 Blue:180 Alpha:255.
		/// </summary>
		public static Colorf SteelBlue
		{
			get { return new Colorf(0.274509803921569f, 0.509803921568627f, 0.705882352941177f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:210 Green:180 Blue:140 Alpha:255.
		/// </summary>
		public static Colorf Tan
		{
			get { return new Colorf(0.823529411764706f, 0.705882352941177f, 0.549019607843137f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:128 Blue:128 Alpha:255.
		/// </summary>
		public static Colorf Teal
		{
			get { return new Colorf(0f, 0.501960784313725f, 0.501960784313725f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:216 Green:191 Blue:216 Alpha:255.
		/// </summary>
		public static Colorf Thistle
		{
			get { return new Colorf(0.847058823529412f, 0.749019607843137f, 0.847058823529412f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:99 Blue:71 Alpha:255.
		/// </summary>
		public static Colorf Tomato
		{
			get { return new Colorf(1f, 0.388235294117647f, 0.27843137254902f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:64 Green:224 Blue:208 Alpha:255.
		/// </summary>
		public static Colorf Turquoise
		{
			get { return new Colorf(0.250980392156863f, 0.87843137254902f, 0.815686274509804f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:238 Green:130 Blue:238 Alpha:255.
		/// </summary>
		public static Colorf Violet
		{
			get { return new Colorf(0.933333333333333f, 0.509803921568627f, 0.933333333333333f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:222 Blue:179 Alpha:255.
		/// </summary>
		public static Colorf Wheat
		{
			get { return new Colorf(0.96078431372549f, 0.870588235294118f, 0.701960784313725f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colorf White
		{
			get { return new Colorf(1f, 1f, 1f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:245 Blue:245 Alpha:255.
		/// </summary>
		public static Colorf WhiteSmoke
		{
			get { return new Colorf(0.96078431372549f, 0.96078431372549f, 0.96078431372549f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:0 Alpha:255.
		/// </summary>
		public static Colorf Yellow
		{
			get { return new Colorf(1f, 1f, 0f, 1f); }
		}
		/// <summary>
		/// Gets a color with the value Red:154 Green:205 Blue:50 Alpha:255.
		/// </summary>
		public static Colorf YellowGreen
		{
			get { return new Colorf(0.603921568627451f, 0.803921568627451f, 0.196078431372549f, 1f); }
		}
		#endregion
		#region Fields
		/// <summary>
		/// The color's red component.
		/// </summary>
		public readonly float R;
		/// <summary>
		/// The color's green component.
		/// </summary>
		public readonly float G;
		/// <summary>
		/// The color's blue component.
		/// </summary>
		public readonly float B;
		/// <summary>
		/// The color's alpha component.
		/// </summary>
		public readonly float A;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this color.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return R;
					case 1:
						return G;
					case 2:
						return B;
					case 3:
						return A;
					default:
						throw new IndexOutOfRangeException("Indices for Colorf run from 0 to 3, inclusive.");
				}
			}
		}
		public float[] ToArray()
		{
			return new float[]
			{
				R, G, B, A
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the color (R, R, R, R).
		/// </summary>
		public Colorf RRRR
		{
			get
			{
				return new Colorf(R, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, R, G).
		/// </summary>
		public Colorf RRRG
		{
			get
			{
				return new Colorf(R, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, R, B).
		/// </summary>
		public Colorf RRRB
		{
			get
			{
				return new Colorf(R, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, R, A).
		/// </summary>
		public Colorf RRRA
		{
			get
			{
				return new Colorf(R, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, R).
		/// </summary>
		public Colorf RRGR
		{
			get
			{
				return new Colorf(R, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, G).
		/// </summary>
		public Colorf RRGG
		{
			get
			{
				return new Colorf(R, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, B).
		/// </summary>
		public Colorf RRGB
		{
			get
			{
				return new Colorf(R, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, A).
		/// </summary>
		public Colorf RRGA
		{
			get
			{
				return new Colorf(R, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, R).
		/// </summary>
		public Colorf RRBR
		{
			get
			{
				return new Colorf(R, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, G).
		/// </summary>
		public Colorf RRBG
		{
			get
			{
				return new Colorf(R, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, B).
		/// </summary>
		public Colorf RRBB
		{
			get
			{
				return new Colorf(R, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, A).
		/// </summary>
		public Colorf RRBA
		{
			get
			{
				return new Colorf(R, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, R).
		/// </summary>
		public Colorf RRAR
		{
			get
			{
				return new Colorf(R, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, G).
		/// </summary>
		public Colorf RRAG
		{
			get
			{
				return new Colorf(R, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, B).
		/// </summary>
		public Colorf RRAB
		{
			get
			{
				return new Colorf(R, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, A).
		/// </summary>
		public Colorf RRAA
		{
			get
			{
				return new Colorf(R, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, R).
		/// </summary>
		public Colorf RGRR
		{
			get
			{
				return new Colorf(R, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, G).
		/// </summary>
		public Colorf RGRG
		{
			get
			{
				return new Colorf(R, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, B).
		/// </summary>
		public Colorf RGRB
		{
			get
			{
				return new Colorf(R, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, A).
		/// </summary>
		public Colorf RGRA
		{
			get
			{
				return new Colorf(R, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, R).
		/// </summary>
		public Colorf RGGR
		{
			get
			{
				return new Colorf(R, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, G).
		/// </summary>
		public Colorf RGGG
		{
			get
			{
				return new Colorf(R, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, B).
		/// </summary>
		public Colorf RGGB
		{
			get
			{
				return new Colorf(R, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, A).
		/// </summary>
		public Colorf RGGA
		{
			get
			{
				return new Colorf(R, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, R).
		/// </summary>
		public Colorf RGBR
		{
			get
			{
				return new Colorf(R, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, G).
		/// </summary>
		public Colorf RGBG
		{
			get
			{
				return new Colorf(R, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, B).
		/// </summary>
		public Colorf RGBB
		{
			get
			{
				return new Colorf(R, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, A).
		/// </summary>
		public Colorf RGBA
		{
			get
			{
				return new Colorf(R, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, R).
		/// </summary>
		public Colorf RGAR
		{
			get
			{
				return new Colorf(R, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, G).
		/// </summary>
		public Colorf RGAG
		{
			get
			{
				return new Colorf(R, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, B).
		/// </summary>
		public Colorf RGAB
		{
			get
			{
				return new Colorf(R, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, A).
		/// </summary>
		public Colorf RGAA
		{
			get
			{
				return new Colorf(R, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, R).
		/// </summary>
		public Colorf RBRR
		{
			get
			{
				return new Colorf(R, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, G).
		/// </summary>
		public Colorf RBRG
		{
			get
			{
				return new Colorf(R, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, B).
		/// </summary>
		public Colorf RBRB
		{
			get
			{
				return new Colorf(R, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, A).
		/// </summary>
		public Colorf RBRA
		{
			get
			{
				return new Colorf(R, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, R).
		/// </summary>
		public Colorf RBGR
		{
			get
			{
				return new Colorf(R, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, G).
		/// </summary>
		public Colorf RBGG
		{
			get
			{
				return new Colorf(R, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, B).
		/// </summary>
		public Colorf RBGB
		{
			get
			{
				return new Colorf(R, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, A).
		/// </summary>
		public Colorf RBGA
		{
			get
			{
				return new Colorf(R, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, R).
		/// </summary>
		public Colorf RBBR
		{
			get
			{
				return new Colorf(R, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, G).
		/// </summary>
		public Colorf RBBG
		{
			get
			{
				return new Colorf(R, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, B).
		/// </summary>
		public Colorf RBBB
		{
			get
			{
				return new Colorf(R, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, A).
		/// </summary>
		public Colorf RBBA
		{
			get
			{
				return new Colorf(R, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, R).
		/// </summary>
		public Colorf RBAR
		{
			get
			{
				return new Colorf(R, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, G).
		/// </summary>
		public Colorf RBAG
		{
			get
			{
				return new Colorf(R, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, B).
		/// </summary>
		public Colorf RBAB
		{
			get
			{
				return new Colorf(R, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, A).
		/// </summary>
		public Colorf RBAA
		{
			get
			{
				return new Colorf(R, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, R).
		/// </summary>
		public Colorf RARR
		{
			get
			{
				return new Colorf(R, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, G).
		/// </summary>
		public Colorf RARG
		{
			get
			{
				return new Colorf(R, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, B).
		/// </summary>
		public Colorf RARB
		{
			get
			{
				return new Colorf(R, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, A).
		/// </summary>
		public Colorf RARA
		{
			get
			{
				return new Colorf(R, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, R).
		/// </summary>
		public Colorf RAGR
		{
			get
			{
				return new Colorf(R, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, G).
		/// </summary>
		public Colorf RAGG
		{
			get
			{
				return new Colorf(R, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, B).
		/// </summary>
		public Colorf RAGB
		{
			get
			{
				return new Colorf(R, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, A).
		/// </summary>
		public Colorf RAGA
		{
			get
			{
				return new Colorf(R, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, R).
		/// </summary>
		public Colorf RABR
		{
			get
			{
				return new Colorf(R, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, G).
		/// </summary>
		public Colorf RABG
		{
			get
			{
				return new Colorf(R, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, B).
		/// </summary>
		public Colorf RABB
		{
			get
			{
				return new Colorf(R, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, A).
		/// </summary>
		public Colorf RABA
		{
			get
			{
				return new Colorf(R, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, R).
		/// </summary>
		public Colorf RAAR
		{
			get
			{
				return new Colorf(R, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, G).
		/// </summary>
		public Colorf RAAG
		{
			get
			{
				return new Colorf(R, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, B).
		/// </summary>
		public Colorf RAAB
		{
			get
			{
				return new Colorf(R, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, A).
		/// </summary>
		public Colorf RAAA
		{
			get
			{
				return new Colorf(R, A, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, R).
		/// </summary>
		public Colorf GRRR
		{
			get
			{
				return new Colorf(G, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, G).
		/// </summary>
		public Colorf GRRG
		{
			get
			{
				return new Colorf(G, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, B).
		/// </summary>
		public Colorf GRRB
		{
			get
			{
				return new Colorf(G, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, A).
		/// </summary>
		public Colorf GRRA
		{
			get
			{
				return new Colorf(G, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, R).
		/// </summary>
		public Colorf GRGR
		{
			get
			{
				return new Colorf(G, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, G).
		/// </summary>
		public Colorf GRGG
		{
			get
			{
				return new Colorf(G, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, B).
		/// </summary>
		public Colorf GRGB
		{
			get
			{
				return new Colorf(G, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, A).
		/// </summary>
		public Colorf GRGA
		{
			get
			{
				return new Colorf(G, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, R).
		/// </summary>
		public Colorf GRBR
		{
			get
			{
				return new Colorf(G, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, G).
		/// </summary>
		public Colorf GRBG
		{
			get
			{
				return new Colorf(G, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, B).
		/// </summary>
		public Colorf GRBB
		{
			get
			{
				return new Colorf(G, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, A).
		/// </summary>
		public Colorf GRBA
		{
			get
			{
				return new Colorf(G, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, R).
		/// </summary>
		public Colorf GRAR
		{
			get
			{
				return new Colorf(G, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, G).
		/// </summary>
		public Colorf GRAG
		{
			get
			{
				return new Colorf(G, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, B).
		/// </summary>
		public Colorf GRAB
		{
			get
			{
				return new Colorf(G, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, A).
		/// </summary>
		public Colorf GRAA
		{
			get
			{
				return new Colorf(G, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, R).
		/// </summary>
		public Colorf GGRR
		{
			get
			{
				return new Colorf(G, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, G).
		/// </summary>
		public Colorf GGRG
		{
			get
			{
				return new Colorf(G, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, B).
		/// </summary>
		public Colorf GGRB
		{
			get
			{
				return new Colorf(G, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, A).
		/// </summary>
		public Colorf GGRA
		{
			get
			{
				return new Colorf(G, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, R).
		/// </summary>
		public Colorf GGGR
		{
			get
			{
				return new Colorf(G, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, G).
		/// </summary>
		public Colorf GGGG
		{
			get
			{
				return new Colorf(G, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, B).
		/// </summary>
		public Colorf GGGB
		{
			get
			{
				return new Colorf(G, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, A).
		/// </summary>
		public Colorf GGGA
		{
			get
			{
				return new Colorf(G, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, R).
		/// </summary>
		public Colorf GGBR
		{
			get
			{
				return new Colorf(G, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, G).
		/// </summary>
		public Colorf GGBG
		{
			get
			{
				return new Colorf(G, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, B).
		/// </summary>
		public Colorf GGBB
		{
			get
			{
				return new Colorf(G, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, A).
		/// </summary>
		public Colorf GGBA
		{
			get
			{
				return new Colorf(G, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, R).
		/// </summary>
		public Colorf GGAR
		{
			get
			{
				return new Colorf(G, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, G).
		/// </summary>
		public Colorf GGAG
		{
			get
			{
				return new Colorf(G, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, B).
		/// </summary>
		public Colorf GGAB
		{
			get
			{
				return new Colorf(G, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, A).
		/// </summary>
		public Colorf GGAA
		{
			get
			{
				return new Colorf(G, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, R).
		/// </summary>
		public Colorf GBRR
		{
			get
			{
				return new Colorf(G, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, G).
		/// </summary>
		public Colorf GBRG
		{
			get
			{
				return new Colorf(G, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, B).
		/// </summary>
		public Colorf GBRB
		{
			get
			{
				return new Colorf(G, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, A).
		/// </summary>
		public Colorf GBRA
		{
			get
			{
				return new Colorf(G, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, R).
		/// </summary>
		public Colorf GBGR
		{
			get
			{
				return new Colorf(G, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, G).
		/// </summary>
		public Colorf GBGG
		{
			get
			{
				return new Colorf(G, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, B).
		/// </summary>
		public Colorf GBGB
		{
			get
			{
				return new Colorf(G, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, A).
		/// </summary>
		public Colorf GBGA
		{
			get
			{
				return new Colorf(G, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, R).
		/// </summary>
		public Colorf GBBR
		{
			get
			{
				return new Colorf(G, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, G).
		/// </summary>
		public Colorf GBBG
		{
			get
			{
				return new Colorf(G, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, B).
		/// </summary>
		public Colorf GBBB
		{
			get
			{
				return new Colorf(G, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, A).
		/// </summary>
		public Colorf GBBA
		{
			get
			{
				return new Colorf(G, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, R).
		/// </summary>
		public Colorf GBAR
		{
			get
			{
				return new Colorf(G, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, G).
		/// </summary>
		public Colorf GBAG
		{
			get
			{
				return new Colorf(G, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, B).
		/// </summary>
		public Colorf GBAB
		{
			get
			{
				return new Colorf(G, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, A).
		/// </summary>
		public Colorf GBAA
		{
			get
			{
				return new Colorf(G, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, R).
		/// </summary>
		public Colorf GARR
		{
			get
			{
				return new Colorf(G, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, G).
		/// </summary>
		public Colorf GARG
		{
			get
			{
				return new Colorf(G, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, B).
		/// </summary>
		public Colorf GARB
		{
			get
			{
				return new Colorf(G, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, A).
		/// </summary>
		public Colorf GARA
		{
			get
			{
				return new Colorf(G, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, R).
		/// </summary>
		public Colorf GAGR
		{
			get
			{
				return new Colorf(G, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, G).
		/// </summary>
		public Colorf GAGG
		{
			get
			{
				return new Colorf(G, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, B).
		/// </summary>
		public Colorf GAGB
		{
			get
			{
				return new Colorf(G, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, A).
		/// </summary>
		public Colorf GAGA
		{
			get
			{
				return new Colorf(G, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, R).
		/// </summary>
		public Colorf GABR
		{
			get
			{
				return new Colorf(G, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, G).
		/// </summary>
		public Colorf GABG
		{
			get
			{
				return new Colorf(G, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, B).
		/// </summary>
		public Colorf GABB
		{
			get
			{
				return new Colorf(G, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, A).
		/// </summary>
		public Colorf GABA
		{
			get
			{
				return new Colorf(G, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, R).
		/// </summary>
		public Colorf GAAR
		{
			get
			{
				return new Colorf(G, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, G).
		/// </summary>
		public Colorf GAAG
		{
			get
			{
				return new Colorf(G, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, B).
		/// </summary>
		public Colorf GAAB
		{
			get
			{
				return new Colorf(G, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, A).
		/// </summary>
		public Colorf GAAA
		{
			get
			{
				return new Colorf(G, A, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, R).
		/// </summary>
		public Colorf BRRR
		{
			get
			{
				return new Colorf(B, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, G).
		/// </summary>
		public Colorf BRRG
		{
			get
			{
				return new Colorf(B, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, B).
		/// </summary>
		public Colorf BRRB
		{
			get
			{
				return new Colorf(B, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, A).
		/// </summary>
		public Colorf BRRA
		{
			get
			{
				return new Colorf(B, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, R).
		/// </summary>
		public Colorf BRGR
		{
			get
			{
				return new Colorf(B, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, G).
		/// </summary>
		public Colorf BRGG
		{
			get
			{
				return new Colorf(B, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, B).
		/// </summary>
		public Colorf BRGB
		{
			get
			{
				return new Colorf(B, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, A).
		/// </summary>
		public Colorf BRGA
		{
			get
			{
				return new Colorf(B, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, R).
		/// </summary>
		public Colorf BRBR
		{
			get
			{
				return new Colorf(B, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, G).
		/// </summary>
		public Colorf BRBG
		{
			get
			{
				return new Colorf(B, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, B).
		/// </summary>
		public Colorf BRBB
		{
			get
			{
				return new Colorf(B, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, A).
		/// </summary>
		public Colorf BRBA
		{
			get
			{
				return new Colorf(B, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, R).
		/// </summary>
		public Colorf BRAR
		{
			get
			{
				return new Colorf(B, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, G).
		/// </summary>
		public Colorf BRAG
		{
			get
			{
				return new Colorf(B, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, B).
		/// </summary>
		public Colorf BRAB
		{
			get
			{
				return new Colorf(B, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, A).
		/// </summary>
		public Colorf BRAA
		{
			get
			{
				return new Colorf(B, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, R).
		/// </summary>
		public Colorf BGRR
		{
			get
			{
				return new Colorf(B, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, G).
		/// </summary>
		public Colorf BGRG
		{
			get
			{
				return new Colorf(B, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, B).
		/// </summary>
		public Colorf BGRB
		{
			get
			{
				return new Colorf(B, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, A).
		/// </summary>
		public Colorf BGRA
		{
			get
			{
				return new Colorf(B, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, R).
		/// </summary>
		public Colorf BGGR
		{
			get
			{
				return new Colorf(B, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, G).
		/// </summary>
		public Colorf BGGG
		{
			get
			{
				return new Colorf(B, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, B).
		/// </summary>
		public Colorf BGGB
		{
			get
			{
				return new Colorf(B, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, A).
		/// </summary>
		public Colorf BGGA
		{
			get
			{
				return new Colorf(B, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, R).
		/// </summary>
		public Colorf BGBR
		{
			get
			{
				return new Colorf(B, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, G).
		/// </summary>
		public Colorf BGBG
		{
			get
			{
				return new Colorf(B, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, B).
		/// </summary>
		public Colorf BGBB
		{
			get
			{
				return new Colorf(B, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, A).
		/// </summary>
		public Colorf BGBA
		{
			get
			{
				return new Colorf(B, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, R).
		/// </summary>
		public Colorf BGAR
		{
			get
			{
				return new Colorf(B, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, G).
		/// </summary>
		public Colorf BGAG
		{
			get
			{
				return new Colorf(B, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, B).
		/// </summary>
		public Colorf BGAB
		{
			get
			{
				return new Colorf(B, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, A).
		/// </summary>
		public Colorf BGAA
		{
			get
			{
				return new Colorf(B, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, R).
		/// </summary>
		public Colorf BBRR
		{
			get
			{
				return new Colorf(B, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, G).
		/// </summary>
		public Colorf BBRG
		{
			get
			{
				return new Colorf(B, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, B).
		/// </summary>
		public Colorf BBRB
		{
			get
			{
				return new Colorf(B, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, A).
		/// </summary>
		public Colorf BBRA
		{
			get
			{
				return new Colorf(B, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, R).
		/// </summary>
		public Colorf BBGR
		{
			get
			{
				return new Colorf(B, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, G).
		/// </summary>
		public Colorf BBGG
		{
			get
			{
				return new Colorf(B, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, B).
		/// </summary>
		public Colorf BBGB
		{
			get
			{
				return new Colorf(B, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, A).
		/// </summary>
		public Colorf BBGA
		{
			get
			{
				return new Colorf(B, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, R).
		/// </summary>
		public Colorf BBBR
		{
			get
			{
				return new Colorf(B, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, G).
		/// </summary>
		public Colorf BBBG
		{
			get
			{
				return new Colorf(B, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, B).
		/// </summary>
		public Colorf BBBB
		{
			get
			{
				return new Colorf(B, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, A).
		/// </summary>
		public Colorf BBBA
		{
			get
			{
				return new Colorf(B, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, R).
		/// </summary>
		public Colorf BBAR
		{
			get
			{
				return new Colorf(B, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, G).
		/// </summary>
		public Colorf BBAG
		{
			get
			{
				return new Colorf(B, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, B).
		/// </summary>
		public Colorf BBAB
		{
			get
			{
				return new Colorf(B, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, A).
		/// </summary>
		public Colorf BBAA
		{
			get
			{
				return new Colorf(B, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, R).
		/// </summary>
		public Colorf BARR
		{
			get
			{
				return new Colorf(B, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, G).
		/// </summary>
		public Colorf BARG
		{
			get
			{
				return new Colorf(B, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, B).
		/// </summary>
		public Colorf BARB
		{
			get
			{
				return new Colorf(B, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, A).
		/// </summary>
		public Colorf BARA
		{
			get
			{
				return new Colorf(B, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, R).
		/// </summary>
		public Colorf BAGR
		{
			get
			{
				return new Colorf(B, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, G).
		/// </summary>
		public Colorf BAGG
		{
			get
			{
				return new Colorf(B, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, B).
		/// </summary>
		public Colorf BAGB
		{
			get
			{
				return new Colorf(B, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, A).
		/// </summary>
		public Colorf BAGA
		{
			get
			{
				return new Colorf(B, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, R).
		/// </summary>
		public Colorf BABR
		{
			get
			{
				return new Colorf(B, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, G).
		/// </summary>
		public Colorf BABG
		{
			get
			{
				return new Colorf(B, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, B).
		/// </summary>
		public Colorf BABB
		{
			get
			{
				return new Colorf(B, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, A).
		/// </summary>
		public Colorf BABA
		{
			get
			{
				return new Colorf(B, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, R).
		/// </summary>
		public Colorf BAAR
		{
			get
			{
				return new Colorf(B, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, G).
		/// </summary>
		public Colorf BAAG
		{
			get
			{
				return new Colorf(B, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, B).
		/// </summary>
		public Colorf BAAB
		{
			get
			{
				return new Colorf(B, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, A).
		/// </summary>
		public Colorf BAAA
		{
			get
			{
				return new Colorf(B, A, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, R).
		/// </summary>
		public Colorf ARRR
		{
			get
			{
				return new Colorf(A, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, G).
		/// </summary>
		public Colorf ARRG
		{
			get
			{
				return new Colorf(A, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, B).
		/// </summary>
		public Colorf ARRB
		{
			get
			{
				return new Colorf(A, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, A).
		/// </summary>
		public Colorf ARRA
		{
			get
			{
				return new Colorf(A, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, R).
		/// </summary>
		public Colorf ARGR
		{
			get
			{
				return new Colorf(A, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, G).
		/// </summary>
		public Colorf ARGG
		{
			get
			{
				return new Colorf(A, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, B).
		/// </summary>
		public Colorf ARGB
		{
			get
			{
				return new Colorf(A, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, A).
		/// </summary>
		public Colorf ARGA
		{
			get
			{
				return new Colorf(A, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, R).
		/// </summary>
		public Colorf ARBR
		{
			get
			{
				return new Colorf(A, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, G).
		/// </summary>
		public Colorf ARBG
		{
			get
			{
				return new Colorf(A, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, B).
		/// </summary>
		public Colorf ARBB
		{
			get
			{
				return new Colorf(A, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, A).
		/// </summary>
		public Colorf ARBA
		{
			get
			{
				return new Colorf(A, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, R).
		/// </summary>
		public Colorf ARAR
		{
			get
			{
				return new Colorf(A, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, G).
		/// </summary>
		public Colorf ARAG
		{
			get
			{
				return new Colorf(A, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, B).
		/// </summary>
		public Colorf ARAB
		{
			get
			{
				return new Colorf(A, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, A).
		/// </summary>
		public Colorf ARAA
		{
			get
			{
				return new Colorf(A, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, R).
		/// </summary>
		public Colorf AGRR
		{
			get
			{
				return new Colorf(A, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, G).
		/// </summary>
		public Colorf AGRG
		{
			get
			{
				return new Colorf(A, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, B).
		/// </summary>
		public Colorf AGRB
		{
			get
			{
				return new Colorf(A, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, A).
		/// </summary>
		public Colorf AGRA
		{
			get
			{
				return new Colorf(A, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, R).
		/// </summary>
		public Colorf AGGR
		{
			get
			{
				return new Colorf(A, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, G).
		/// </summary>
		public Colorf AGGG
		{
			get
			{
				return new Colorf(A, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, B).
		/// </summary>
		public Colorf AGGB
		{
			get
			{
				return new Colorf(A, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, A).
		/// </summary>
		public Colorf AGGA
		{
			get
			{
				return new Colorf(A, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, R).
		/// </summary>
		public Colorf AGBR
		{
			get
			{
				return new Colorf(A, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, G).
		/// </summary>
		public Colorf AGBG
		{
			get
			{
				return new Colorf(A, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, B).
		/// </summary>
		public Colorf AGBB
		{
			get
			{
				return new Colorf(A, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, A).
		/// </summary>
		public Colorf AGBA
		{
			get
			{
				return new Colorf(A, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, R).
		/// </summary>
		public Colorf AGAR
		{
			get
			{
				return new Colorf(A, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, G).
		/// </summary>
		public Colorf AGAG
		{
			get
			{
				return new Colorf(A, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, B).
		/// </summary>
		public Colorf AGAB
		{
			get
			{
				return new Colorf(A, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, A).
		/// </summary>
		public Colorf AGAA
		{
			get
			{
				return new Colorf(A, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, R).
		/// </summary>
		public Colorf ABRR
		{
			get
			{
				return new Colorf(A, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, G).
		/// </summary>
		public Colorf ABRG
		{
			get
			{
				return new Colorf(A, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, B).
		/// </summary>
		public Colorf ABRB
		{
			get
			{
				return new Colorf(A, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, A).
		/// </summary>
		public Colorf ABRA
		{
			get
			{
				return new Colorf(A, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, R).
		/// </summary>
		public Colorf ABGR
		{
			get
			{
				return new Colorf(A, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, G).
		/// </summary>
		public Colorf ABGG
		{
			get
			{
				return new Colorf(A, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, B).
		/// </summary>
		public Colorf ABGB
		{
			get
			{
				return new Colorf(A, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, A).
		/// </summary>
		public Colorf ABGA
		{
			get
			{
				return new Colorf(A, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, R).
		/// </summary>
		public Colorf ABBR
		{
			get
			{
				return new Colorf(A, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, G).
		/// </summary>
		public Colorf ABBG
		{
			get
			{
				return new Colorf(A, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, B).
		/// </summary>
		public Colorf ABBB
		{
			get
			{
				return new Colorf(A, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, A).
		/// </summary>
		public Colorf ABBA
		{
			get
			{
				return new Colorf(A, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, R).
		/// </summary>
		public Colorf ABAR
		{
			get
			{
				return new Colorf(A, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, G).
		/// </summary>
		public Colorf ABAG
		{
			get
			{
				return new Colorf(A, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, B).
		/// </summary>
		public Colorf ABAB
		{
			get
			{
				return new Colorf(A, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, A).
		/// </summary>
		public Colorf ABAA
		{
			get
			{
				return new Colorf(A, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, R).
		/// </summary>
		public Colorf AARR
		{
			get
			{
				return new Colorf(A, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, G).
		/// </summary>
		public Colorf AARG
		{
			get
			{
				return new Colorf(A, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, B).
		/// </summary>
		public Colorf AARB
		{
			get
			{
				return new Colorf(A, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, A).
		/// </summary>
		public Colorf AARA
		{
			get
			{
				return new Colorf(A, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, R).
		/// </summary>
		public Colorf AAGR
		{
			get
			{
				return new Colorf(A, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, G).
		/// </summary>
		public Colorf AAGG
		{
			get
			{
				return new Colorf(A, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, B).
		/// </summary>
		public Colorf AAGB
		{
			get
			{
				return new Colorf(A, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, A).
		/// </summary>
		public Colorf AAGA
		{
			get
			{
				return new Colorf(A, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, R).
		/// </summary>
		public Colorf AABR
		{
			get
			{
				return new Colorf(A, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, G).
		/// </summary>
		public Colorf AABG
		{
			get
			{
				return new Colorf(A, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, B).
		/// </summary>
		public Colorf AABB
		{
			get
			{
				return new Colorf(A, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, A).
		/// </summary>
		public Colorf AABA
		{
			get
			{
				return new Colorf(A, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, R).
		/// </summary>
		public Colorf AAAR
		{
			get
			{
				return new Colorf(A, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, G).
		/// </summary>
		public Colorf AAAG
		{
			get
			{
				return new Colorf(A, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, B).
		/// </summary>
		public Colorf AAAB
		{
			get
			{
				return new Colorf(A, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, A).
		/// </summary>
		public Colorf AAAA
		{
			get
			{
				return new Colorf(A, A, A, A);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Colorf"/> using the specified values.
		/// </summary>
		/// <param name="red">Value for the red component of the color.</param>
		/// <param name="green">Value for the green component of the color.</param>
		/// <param name="blue">Value for the blue component of the color.</param>
		public Colorf(float red, float green, float blue)
			: this(red, green, blue, 1)
		{
			Contract.Requires(0 <= red);
			Contract.Requires(0 <= green);
			Contract.Requires(0 <= blue);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Colorf"/> using the specified values.
		/// </summary>
		/// <param name="red">Value for the red component of the color.</param>
		/// <param name="green">Value for the green component of the color.</param>
		/// <param name="blue">Value for the blue component of the color.</param>
		/// <param name="alpha">Value for the alpha component of the color.</param>
		public Colorf(float red, float green, float blue, float alpha)
		{
		Contract.Requires(0 <= red);
		Contract.Requires(0 <= green);
		Contract.Requires(0 <= blue);
		Contract.Requires(0 <= alpha);
			R = red;
			G = green;
			B = blue;
			A = alpha;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Colorf"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Colorf(float[] array)
			: this(array, 0)
		{
			Contract.Requires(array != null);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Colorf"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Colorf(float[] array, int offset)
		{
			Contract.Requires(array != null);
			if (array.Length < offset + 3)
			{
				throw new ArgumentException("Not enough elements in array.", "array");
			}
			R = array[offset];
			G = array[offset + 1];
			B = array[offset + 2];
			if (array.Length < offset + 4)
			{
				A = 1;
			}
			else
			{
				A = array[offset + 3];
			}
		}
		#endregion
		#region Operations
		/// <summary>
		/// Returns the identity of a specified color.
		/// </summary>
		/// <param name="value">A color.</param>
		/// <returns>The identity of value.</returns>
		public static Colorf operator +(Colorf value)
		{
			return value;
		}
		/// <summary>
		/// Adds two colors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Colorf operator +(Colorf left, Colorf right)
		{
			return Color.Add(left, right);
		}
		/// <summary>
		/// Subtracts one color from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Colorf operator -(Colorf left, Colorf right)
		{
			return Color.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a color and scalar.
		/// </summary>
		/// <param name="left">The color to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Colorf operator *(Colorf left, float right)
		{
			return Color.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and color.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The color to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Colorf operator *(float left, Colorf right)
		{
			return Color.Multiply(right, left);
		}
		/// <summary>
		/// Divides a color by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The color to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Colorf operator /(Colorf left, float right)
		{
			return Color.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static explicit operator Colorf(Colord value)
		{
			return new Colorf((float)value.R, (float)value.G, (float)value.B, (float)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Colorf value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Colorf value)
		{
			return new Vector4d((double)value.R, (double)value.G, (double)value.B, (double)value.A);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector4d value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector4d value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an implicit conversion of a Colorf value to a Vector4f.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4f.</param>
		/// <returns>A Vector4f that has all components equal to value.</returns>
		public static implicit operator Vector4f(Colorf value)
		{
			return new Vector4f((float)value.R, (float)value.G, (float)value.B, (float)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4f value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static implicit operator Colorf(Vector4f value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Colorf value)
		{
			return new Vector4h((Half)value.R, (Half)value.G, (Half)value.B, (Half)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4h value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static implicit operator Colorf(Vector4h value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4ul.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4ul.</param>
		/// <returns>A Vector4ul that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4ul(Colorf value)
		{
			return new Vector4ul((ulong)value.R, (ulong)value.G, (ulong)value.B, (ulong)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ul value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colorf(Vector4ul value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static explicit operator Vector4l(Colorf value)
		{
			return new Vector4l((long)value.R, (long)value.G, (long)value.B, (long)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4l value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static implicit operator Colorf(Vector4l value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4ui.</param>
		/// <returns>A Vector4ui that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4ui(Colorf value)
		{
			return new Vector4ui((uint)value.R, (uint)value.G, (uint)value.B, (uint)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ui value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colorf(Vector4ui value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static explicit operator Vector4i(Colorf value)
		{
			return new Vector4i((int)value.R, (int)value.G, (int)value.B, (int)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4i value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static implicit operator Colorf(Vector4i value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4us(Colorf value)
		{
			return new Vector4us((ushort)value.R, (ushort)value.G, (ushort)value.B, (ushort)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4us value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colorf(Vector4us value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Colorf value)
		{
			return new Vector4s((short)value.R, (short)value.G, (short)value.B, (short)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4s value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static implicit operator Colorf(Vector4s value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4b.</param>
		/// <returns>A Vector4b that has all components equal to value.</returns>
		public static explicit operator Vector4b(Colorf value)
		{
			return new Vector4b((byte)value.R, (byte)value.G, (byte)value.B, (byte)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		public static implicit operator Colorf(Vector4b value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4sb(Colorf value)
		{
			return new Vector4sb((sbyte)value.R, (sbyte)value.G, (sbyte)value.B, (sbyte)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4sb value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A Colorf that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colorf(Vector4sb value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3d.</param>
		/// <returns>A Vector3d that has all components equal to value.</returns>
		public static explicit operator Vector3d(Colorf value)
		{
			return new Vector3d((double)value.R, (double)value.G, (double)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3d value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3f.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3f.</param>
		/// <returns>A Vector3f that has all components equal to value.</returns>
		public static explicit operator Vector3f(Colorf value)
		{
			return new Vector3f((float)value.R, (float)value.G, (float)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3f value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3h.</param>
		/// <returns>A Vector3h that has all components equal to value.</returns>
		public static explicit operator Vector3h(Colorf value)
		{
			return new Vector3h((Half)value.R, (Half)value.G, (Half)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3h value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3h value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3ul.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ul.</param>
		/// <returns>A Vector3ul that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3ul(Colorf value)
		{
			return new Vector3ul((ulong)value.R, (ulong)value.G, (ulong)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ul value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colorf(Vector3ul value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static explicit operator Vector3l(Colorf value)
		{
			return new Vector3l((long)value.R, (long)value.G, (long)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3l value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3l value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3ui(Colorf value)
		{
			return new Vector3ui((uint)value.R, (uint)value.G, (uint)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ui value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colorf(Vector3ui value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3i.</param>
		/// <returns>A Vector3i that has all components equal to value.</returns>
		public static explicit operator Vector3i(Colorf value)
		{
			return new Vector3i((int)value.R, (int)value.G, (int)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3i value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3i value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3us.</param>
		/// <returns>A Vector3us that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3us(Colorf value)
		{
			return new Vector3us((ushort)value.R, (ushort)value.G, (ushort)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3us value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colorf(Vector3us value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Colorf value)
		{
			return new Vector3s((short)value.R, (short)value.G, (short)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3s value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3s value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3b.</param>
		/// <returns>A Vector3b that has all components equal to value.</returns>
		public static explicit operator Vector3b(Colorf value)
		{
			return new Vector3b((byte)value.R, (byte)value.G, (byte)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3b value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colorf(Vector3b value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colorf value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3sb(Colorf value)
		{
			return new Vector3sb((sbyte)value.R, (sbyte)value.G, (sbyte)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3sb value to a Colorf.
		/// </summary>
		/// <param name="value">The value to convert to a Colorf.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colorf(Vector3sb value)
		{
			return new Colorf((float)value.X, (float)value.Y, (float)value.Z, 1);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Colorf"/>.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		public override int GetHashCode()
		{
			return R.GetHashCode() + G.GetHashCode() + B.GetHashCode() + A.GetHashCode();
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// object have the same value.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>true if the obj parameter is a <see cref="Colorf"/> object or a type capable
		/// of implicit conversion to a <see cref="Colorf"/> object, and its value
		/// is equal to the current <see cref="Colorf"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Colorf) { return Equals((Colorf)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Colorf other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two colors are equal.
		/// </summary>
		/// <param name="left">The first color to compare.</param>
		/// <param name="right">The second color to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Colorf left, Colorf right)
		{
			return left.R == right.R & left.G == right.G & left.B == right.B & left.A == right.A;
		}
		/// <summary>
		/// Returns a value that indicates whether two colors are not equal.
		/// </summary>
		/// <param name="left">The first color to compare.</param>
		/// <param name="right">The second color to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Colorf left, Colorf right)
		{
			return left.R != right.R | left.G != right.G | left.B != right.B | left.A != right.A;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts the value of the current color to its equivalent string representation.
		/// </summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current color to its equivalent string
		/// representation by using the specified culture-specific
		/// formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance, as specified
		/// by provider.</returns>
		public string ToString(IFormatProvider provider)
		{
			return ToString("G", provider);
		}
		/// <summary>
		/// Converts the value of the current color to its equivalent string
		/// representation by using the specified format for its components.
		/// formatting information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}
		/// <summary>
		/// Converts the value of the current color to its equivalent string
		/// representation by using the specified format and culture-specific
		/// format information for its components.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the current instance, as specified
		/// by format and provider.</returns>
		/// <exception cref="System.FormatException">format is not a valid format string.</exception>
		public string ToString(string format, IFormatProvider provider)
		{
			return String.Format("Red:{0} Green:{1} Blue:{2} Alpha:{3}", R.ToString(format, provider), G.ToString(format, provider), B.ToString(format, provider), A.ToString(format, provider));
		}
		#endregion
	}
	/// <summary>
	/// Provides static methods for color functions.
	/// </summary>
	public static partial class Color
	{
		#region Binary
		/// <summary>
		/// Writes the given <see cref="Colorf"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Colorf vector)
		{
			writer.Write(vector.R);
			writer.Write(vector.G);
			writer.Write(vector.B);
			writer.Write(vector.A);
		}
		/// <summary>
		/// Reads a <see cref="Colorf"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Colorf ReadColorf(this Ibasa.IO.BinaryReader reader)
		{
			return new Colorf(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds two colors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Colorf Add(Colorf left, Colorf right)
		{
			return new Colorf(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
		}
		/// <summary>
		/// Subtracts one colors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Colorf Subtract(Colorf left, Colorf right)
		{
			return new Colorf(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
		}
		/// <summary>
		/// Returns the product of a color and scalar.
		/// </summary>
		/// <param name="color">The color to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Colorf Multiply(Colorf color, float scalar)
		{
			return new Colorf(color.R * scalar, color.G * scalar, color.B * scalar, color.A * scalar);
		}
		/// <summary>
		/// Divides a color by a scalar and returns the result.
		/// </summary>
		/// <param name="color">The color to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Colorf Divide(Colorf color, float scalar)
		{
			return new Colorf(color.R / scalar, color.G / scalar, color.B / scalar, color.A / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two colors are equal.
		/// </summary>
		/// <param name="left">The first color to compare.</param>
		/// <param name="right">The second color to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Colorf left, Colorf right)
		{
			return left == right;
		}
		#endregion
		#region Test
		/// <summary>
		/// Determines whether all components of a color are non-zero.
		/// </summary>
		/// <param name="value">A color.</param>
		/// <returns>true if all components are non-zero; false otherwise.</returns>
		public static bool All(Colorf value)
		{
			return value.R != 0 && value.G != 0 && value.B != 0 && value.A != 0;
		}
		/// <summary>
		/// Determines whether all components of a color satisfy a condition.
		/// </summary>
		/// <param name="value">A color.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if every component of the color passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool All(Colorf value, Predicate<float> predicate)
		{
			return predicate(value.R) && predicate(value.G) && predicate(value.B) && predicate(value.A);
		}
		/// <summary>
		/// Determines whether any component of a color is non-zero.
		/// </summary>
		/// <param name="value">A color.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Colorf value)
		{
			return value.R != 0 || value.G != 0 || value.B != 0 || value.A != 0;
		}
		/// <summary>
		/// Determines whether any components of a color satisfy a condition.
		/// </summary>
		/// <param name="value">A color.</param>
		/// <param name="predicate">A function to test each component for a condition.</param>
		/// <returns>true if any component of the color passes the test in the specified
		/// predicate; otherwise, false.</returns>
		public static bool Any(Colorf value, Predicate<float> predicate)
		{
			return predicate(value.R) || predicate(value.G) || predicate(value.B) || predicate(value.A);
		}
		#endregion
		#region Negative, Premultiply and Normalize
		/// <summary>
		/// Returns the color negative of a normalized color.
		/// </summary>
		/// <param name="color">A normalized color.</param>
		/// <returns>The negative of color.</returns>
		public static Colorf Negative(Colorf color)
		{
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().R && Contract.Result<Colorf>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().G && Contract.Result<Colorf>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().B && Contract.Result<Colorf>().B <= 1, "Result must be normalized.");
			return new Colorf(1 - color.R, 1 - color.G, 1 - color.B, color.A);
		}
		/// <summary>
		/// Multiplies the RGB values of the color by the alpha value.
		/// </summary>
		/// <param name="color">The color to premultiply.</param>
		/// <returns>The premultipled color.</returns>
		public static Colorf Premultiply(Colorf color)
		{
			return new Colorf(color.R * color.A, color.G * color.A, color.B * color.A, color.A);
		}
		/// <summary>
		/// Normalizes a color so all its RGB values are in the range [0.0, 1.0].
		/// </summary>
		/// <param name="color">The color to normalize.</param>
		/// <returns>The normalized color.</returns>
		public static Colorf Normalize(Colorf color)
		{
			Contract.Ensures(0 <= Contract.Result<Colorf>().R && Contract.Result<Colorf>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().G && Contract.Result<Colorf>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().B && Contract.Result<Colorf>().B <= 1, "Result must be normalized.");
			var bias = Functions.Min(Functions.Min(Functions.Min(color.R, color.G), color.B), 0);
			color -= new Colorf(bias, bias, bias, 0);
			var scale = Functions.Max(Functions.Max(Functions.Max(color.R, color.G), color.B), 1);
			return color / scale;
		}
		#endregion
		#region Colorspace
		/// <summary>
		/// Converts a color to greyscale.
		/// </summary>
		/// <param name="color">The color to convert.</param>
		/// <returns>color in greyscale.</returns>
		public static Colorf Greyscale(Colorf color)
		{
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().R && Contract.Result<Colorf>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().G && Contract.Result<Colorf>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().B && Contract.Result<Colorf>().B <= 1, "Result must be normalized.");
			var greyscale = 0.2125f * color.R + 0.7154f * color.G + 0.0721f * color.B;
			return new Colorf(greyscale, greyscale, greyscale, color.A);
		}
		/// <summary>
		/// Converts a color to black or white.
		/// </summary>
		/// <param name="color">The color to convert.</param>
		/// <returns>color in black or white.</returns>
		public static Colorf BlackWhite(Colorf color)
		{
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().R && Contract.Result<Colorf>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().G && Contract.Result<Colorf>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colorf>().B && Contract.Result<Colorf>().B <= 1, "Result must be normalized.");
			var bw = Functions.Round(0.2125f * color.R + 0.7154f * color.G + 0.0721f * color.B);
			return new Colorf(bw, bw, bw, color.A);
		}
		/// <summary>
		/// Gamma correct a color.
		/// </summary>
		/// <param name="color">Color to gamma correct.</param>
		/// <param name="gamma">Gamma value to use.</param>
		/// <returns>The gamma corrected color.</returns>
		public static Colorf Gamma(Colorf color, float gamma)
		{
			var r = Functions.Pow(color.R, gamma);
			var g = Functions.Pow(color.G, gamma);
			var b = Functions.Pow(color.B, gamma);
			var a = Functions.Pow(color.A, gamma);
			return new Colorf(r, g, b, a);
		}
		#endregion
		#region Quantization
		public static Vector4l Quantize(int redBits, int greenBits, int blueBits, int alphaBits, Colorf color)
		{
			Contract.Requires(0 <= redBits && redBits <= 63, "redBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= greenBits && greenBits <= 63, "greenBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= blueBits && blueBits <= 63, "blueBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= alphaBits && alphaBits <= 63, "alphaBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.A && color.A <= 1, "color must be normalized.");
			Contract.Requires(0 <= Contract.Result<Vector4l>().X, "result must be positive.");
			Contract.Requires(0 <= Contract.Result<Vector4l>().Y, "result must be positive.");
			Contract.Requires(0 <= Contract.Result<Vector4l>().Z, "result must be positive.");
			Contract.Requires(0 <= Contract.Result<Vector4l>().W, "result must be positive.");
			long r = (long)(color.R * long.MaxValue);
			long g = (long)(color.G * long.MaxValue);
			long b = (long)(color.B * long.MaxValue);
			long a = (long)(color.A * long.MaxValue);
			r >>= (63 - redBits);
			g >>= (63 - greenBits);
			b >>= (63 - blueBits);
			a >>= (63 - alphaBits);
			return new Vector4l(r, g, b, a);
		}
		public static Vector3l Quantize(int redBits, int greenBits, int blueBits, Colorf color)
		{
			Contract.Requires(0 <= redBits && redBits <= 63, "redBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= greenBits && greenBits <= 63, "greenBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= blueBits && blueBits <= 63, "blueBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Requires(0 <= Contract.Result<Vector3l>().X, "result must be positive.");
			Contract.Requires(0 <= Contract.Result<Vector3l>().Y, "result must be positive.");
			Contract.Requires(0 <= Contract.Result<Vector3l>().Z, "result must be positive.");
			long r = (long)(color.R * long.MaxValue);
			long g = (long)(color.G * long.MaxValue);
			long b = (long)(color.B * long.MaxValue);
			r >>= (63 - redBits);
			g >>= (63 - greenBits);
			b >>= (63 - blueBits);
			return new Vector3l(r, g, b);
		}
		public static Colorf Unquantizef(int redBits, int greenBits, int blueBits, int alphaBits, Vector4l color)
		{
			Contract.Requires(0 <= redBits && redBits <= 63, "redBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= greenBits && greenBits <= 63, "greenBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= blueBits && blueBits <= 63, "blueBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= alphaBits && alphaBits <= 63, "alphaBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= color.X, "color must be positive.");
			Contract.Requires(0 <= color.Y, "color must be positive.");
			Contract.Requires(0 <= color.Z, "color must be positive.");
			Contract.Requires(0 <= color.W, "color must be positive.");
			Contract.Requires(0 <= Contract.Result<Colorf>().R && Contract.Result<Colorf>().R <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colorf>().G && Contract.Result<Colorf>().G <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colorf>().B && Contract.Result<Colorf>().B <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colorf>().A && Contract.Result<Colorf>().A <= 1, "result must be normalized.");
			var r = (float)color.X / ((1L << redBits) - 1);
			var g = (float)color.Y / ((1L << greenBits) - 1);
			var b = (float)color.Z / ((1L << blueBits) - 1);
			var a = (float)color.W / ((1L << alphaBits) - 1);
			return new Colorf(r, g, b, a);
		}
		public static Colorf Unquantizef(int redBits, int greenBits, int blueBits, Vector3l color)
		{
			Contract.Requires(0 <= redBits && redBits <= 63, "redBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= greenBits && greenBits <= 63, "greenBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= blueBits && blueBits <= 63, "blueBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= color.X, "color must be positive.");
			Contract.Requires(0 <= color.Y, "color must be positive.");
			Contract.Requires(0 <= color.Z, "color must be positive.");
			Contract.Requires(0 <= Contract.Result<Colorf>().R && Contract.Result<Colorf>().R <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colorf>().G && Contract.Result<Colorf>().G <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colorf>().B && Contract.Result<Colorf>().B <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colorf>().A && Contract.Result<Colorf>().A <= 1, "result must be normalized.");
			var r = (float)color.X / ((1L << redBits) - 1);
			var g = (float)color.Y / ((1L << greenBits) - 1);
			var b = (float)color.Z / ((1L << blueBits) - 1);
			return new Colorf(r, g, b, 1);
		}
		#endregion
		#region Per component
		#region Transform
		/// <summary>
		/// Transforms the components of a color and returns the result.
		/// </summary>
		/// <param name="value">The color to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Colord Transform(Colorf value, Func<float, double> transformer)
		{
			return new Colord(transformer(value.R), transformer(value.G), transformer(value.B), transformer(value.A));
		}
		/// <summary>
		/// Transforms the components of a color and returns the result.
		/// </summary>
		/// <param name="value">The color to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Colorf Transform(Colorf value, Func<float, float> transformer)
		{
			return new Colorf(transformer(value.R), transformer(value.G), transformer(value.B), transformer(value.A));
		}
		#endregion
		/// <summary>
		/// Multiplys the components of two colors and returns the result.
		/// </summary>
		/// <param name="left">The first color to modulate.</param>
		/// <param name="right">The second color to modulate.</param>
		/// <returns>The result of multiplying each component of left by the matching component in right.</returns>
		public static Colorf Modulate(Colorf left, Colorf right)
		{
			return new Colorf(left.R * right.R, left.G * right.G, left.B * right.B, left.A * right.A);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A color.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Colorf Abs(Colorf value)
		{
			return new Colorf(Functions.Abs(value.R), Functions.Abs(value.G), Functions.Abs(value.B), Functions.Abs(value.A));
		}
		/// <summary>
		/// Returns a color that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first color.</param>
		/// <param name="value2">The second color.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Colorf Min(Colorf value1, Colorf value2)
		{
			return new Colorf(Functions.Min(value1.R, value2.R), Functions.Min(value1.G, value2.G), Functions.Min(value1.B, value2.B), Functions.Min(value1.A, value2.A));
		}
		/// <summary>
		/// Returns a color that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first color.</param>
		/// <param name="value2">The second color.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Colorf Max(Colorf value1, Colorf value2)
		{
			return new Colorf(Functions.Max(value1.R, value2.R), Functions.Max(value1.G, value2.G), Functions.Max(value1.B, value2.B), Functions.Max(value1.A, value2.A));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A color to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A color with each component constrained to the given range.</returns>
		public static Colorf Clamp(Colorf value, Colorf min, Colorf max)
		{
			return new Colorf(Functions.Clamp(value.R, min.R, max.R), Functions.Clamp(value.G, min.G, max.G), Functions.Clamp(value.B, min.B, max.B), Functions.Clamp(value.A, min.A, max.A));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A color to saturate.</param>
		/// <returns>A color with each component constrained to the range 0 to 1.</returns>
		public static Colorf Saturate(Colorf value)
		{
			return new Colorf(Functions.Saturate(value.R), Functions.Saturate(value.G), Functions.Saturate(value.B), Functions.Saturate(value.A));
		}
		#endregion
		#region Interpolation
		/// <summary>
		/// Performs a linear interpolation between two colors.
		/// </summary>
		/// <param name="color1">First color.</param>
		/// <param name="color2">Second color.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of <paramref name="color2"/>.</param>
		/// <returns>The linear interpolation of the two values.</returns>
		public static Colorf Lerp(Colorf color1, Colorf color2, float amount)
		{
			return (1 - amount) * color1 + amount * color2;
		}
		#endregion
	}
}
