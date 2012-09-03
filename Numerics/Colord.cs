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
	public struct Colord: IEquatable<Colord>, IFormattable
	{
		#region Constants
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:255 Alpha:0.
		/// </summary>
		public static Colord Transparent
		{
			get { return new Colord(1, 1, 1, 0); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:248 Blue:255 Alpha:255.
		/// </summary>
		public static Colord AliceBlue
		{
			get { return new Colord(0.941176470588235, 0.972549019607843, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:235 Blue:215 Alpha:255.
		/// </summary>
		public static Colord AntiqueWhite
		{
			get { return new Colord(0.980392156862745, 0.92156862745098, 0.843137254901961, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colord Aqua
		{
			get { return new Colord(0, 1, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:127 Green:255 Blue:212 Alpha:255.
		/// </summary>
		public static Colord Aquamarine
		{
			get { return new Colord(0.498039215686275, 1, 0.831372549019608, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colord Azure
		{
			get { return new Colord(0.941176470588235, 1, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:245 Blue:220 Alpha:255.
		/// </summary>
		public static Colord Beige
		{
			get { return new Colord(0.96078431372549, 0.96078431372549, 0.862745098039216, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:228 Blue:196 Alpha:255.
		/// </summary>
		public static Colord Bisque
		{
			get { return new Colord(1, 0.894117647058824, 0.768627450980392, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Black
		{
			get { return new Colord(0, 0, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:235 Blue:205 Alpha:255.
		/// </summary>
		public static Colord BlanchedAlmond
		{
			get { return new Colord(1, 0.92156862745098, 0.803921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:255 Alpha:255.
		/// </summary>
		public static Colord Blue
		{
			get { return new Colord(0, 0, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:138 Green:43 Blue:226 Alpha:255.
		/// </summary>
		public static Colord BlueViolet
		{
			get { return new Colord(0.541176470588235, 0.168627450980392, 0.886274509803922, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:165 Green:42 Blue:42 Alpha:255.
		/// </summary>
		public static Colord Brown
		{
			get { return new Colord(0.647058823529412, 0.164705882352941, 0.164705882352941, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:222 Green:184 Blue:135 Alpha:255.
		/// </summary>
		public static Colord BurlyWood
		{
			get { return new Colord(0.870588235294118, 0.72156862745098, 0.529411764705882, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:95 Green:158 Blue:160 Alpha:255.
		/// </summary>
		public static Colord CadetBlue
		{
			get { return new Colord(0.372549019607843, 0.619607843137255, 0.627450980392157, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:127 Green:255 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Chartreuse
		{
			get { return new Colord(0.498039215686275, 1, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:210 Green:105 Blue:30 Alpha:255.
		/// </summary>
		public static Colord Chocolate
		{
			get { return new Colord(0.823529411764706, 0.411764705882353, 0.117647058823529, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:127 Blue:80 Alpha:255.
		/// </summary>
		public static Colord Coral
		{
			get { return new Colord(1, 0.498039215686275, 0.313725490196078, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:100 Green:149 Blue:237 Alpha:255.
		/// </summary>
		public static Colord CornflowerBlue
		{
			get { return new Colord(0.392156862745098, 0.584313725490196, 0.929411764705882, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:248 Blue:220 Alpha:255.
		/// </summary>
		public static Colord Cornsilk
		{
			get { return new Colord(1, 0.972549019607843, 0.862745098039216, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:220 Green:20 Blue:60 Alpha:255.
		/// </summary>
		public static Colord Crimson
		{
			get { return new Colord(0.862745098039216, 0.0784313725490196, 0.235294117647059, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colord Cyan
		{
			get { return new Colord(0, 1, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:139 Alpha:255.
		/// </summary>
		public static Colord DarkBlue
		{
			get { return new Colord(0, 0, 0.545098039215686, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:139 Blue:139 Alpha:255.
		/// </summary>
		public static Colord DarkCyan
		{
			get { return new Colord(0, 0.545098039215686, 0.545098039215686, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:184 Green:134 Blue:11 Alpha:255.
		/// </summary>
		public static Colord DarkGoldenrod
		{
			get { return new Colord(0.72156862745098, 0.525490196078431, 0.0431372549019608, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:169 Green:169 Blue:169 Alpha:255.
		/// </summary>
		public static Colord DarkGray
		{
			get { return new Colord(0.662745098039216, 0.662745098039216, 0.662745098039216, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:100 Blue:0 Alpha:255.
		/// </summary>
		public static Colord DarkGreen
		{
			get { return new Colord(0, 0.392156862745098, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:189 Green:183 Blue:107 Alpha:255.
		/// </summary>
		public static Colord DarkKhaki
		{
			get { return new Colord(0.741176470588235, 0.717647058823529, 0.419607843137255, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:139 Green:0 Blue:139 Alpha:255.
		/// </summary>
		public static Colord DarkMagenta
		{
			get { return new Colord(0.545098039215686, 0, 0.545098039215686, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:85 Green:107 Blue:47 Alpha:255.
		/// </summary>
		public static Colord DarkOliveGreen
		{
			get { return new Colord(0.333333333333333, 0.419607843137255, 0.184313725490196, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:140 Blue:0 Alpha:255.
		/// </summary>
		public static Colord DarkOrange
		{
			get { return new Colord(1, 0.549019607843137, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:153 Green:50 Blue:204 Alpha:255.
		/// </summary>
		public static Colord DarkOrchid
		{
			get { return new Colord(0.6, 0.196078431372549, 0.8, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:139 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colord DarkRed
		{
			get { return new Colord(0.545098039215686, 0, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:233 Green:150 Blue:122 Alpha:255.
		/// </summary>
		public static Colord DarkSalmon
		{
			get { return new Colord(0.913725490196078, 0.588235294117647, 0.47843137254902, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:143 Green:188 Blue:139 Alpha:255.
		/// </summary>
		public static Colord DarkSeaGreen
		{
			get { return new Colord(0.56078431372549, 0.737254901960784, 0.545098039215686, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:72 Green:61 Blue:139 Alpha:255.
		/// </summary>
		public static Colord DarkSlateBlue
		{
			get { return new Colord(0.282352941176471, 0.23921568627451, 0.545098039215686, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:47 Green:79 Blue:79 Alpha:255.
		/// </summary>
		public static Colord DarkSlateGray
		{
			get { return new Colord(0.184313725490196, 0.309803921568627, 0.309803921568627, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:206 Blue:209 Alpha:255.
		/// </summary>
		public static Colord DarkTurquoise
		{
			get { return new Colord(0, 0.807843137254902, 0.819607843137255, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:148 Green:0 Blue:211 Alpha:255.
		/// </summary>
		public static Colord DarkViolet
		{
			get { return new Colord(0.580392156862745, 0, 0.827450980392157, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:20 Blue:147 Alpha:255.
		/// </summary>
		public static Colord DeepPink
		{
			get { return new Colord(1, 0.0784313725490196, 0.576470588235294, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:191 Blue:255 Alpha:255.
		/// </summary>
		public static Colord DeepSkyBlue
		{
			get { return new Colord(0, 0.749019607843137, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:105 Green:105 Blue:105 Alpha:255.
		/// </summary>
		public static Colord DimGray
		{
			get { return new Colord(0.411764705882353, 0.411764705882353, 0.411764705882353, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:30 Green:144 Blue:255 Alpha:255.
		/// </summary>
		public static Colord DodgerBlue
		{
			get { return new Colord(0.117647058823529, 0.564705882352941, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:178 Green:34 Blue:34 Alpha:255.
		/// </summary>
		public static Colord Firebrick
		{
			get { return new Colord(0.698039215686274, 0.133333333333333, 0.133333333333333, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:250 Blue:240 Alpha:255.
		/// </summary>
		public static Colord FloralWhite
		{
			get { return new Colord(1, 0.980392156862745, 0.941176470588235, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:34 Green:139 Blue:34 Alpha:255.
		/// </summary>
		public static Colord ForestGreen
		{
			get { return new Colord(0.133333333333333, 0.545098039215686, 0.133333333333333, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:0 Blue:255 Alpha:255.
		/// </summary>
		public static Colord Fuchsia
		{
			get { return new Colord(1, 0, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:220 Green:220 Blue:220 Alpha:255.
		/// </summary>
		public static Colord Gainsboro
		{
			get { return new Colord(0.862745098039216, 0.862745098039216, 0.862745098039216, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:248 Green:248 Blue:255 Alpha:255.
		/// </summary>
		public static Colord GhostWhite
		{
			get { return new Colord(0.972549019607843, 0.972549019607843, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:215 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Gold
		{
			get { return new Colord(1, 0.843137254901961, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:218 Green:165 Blue:32 Alpha:255.
		/// </summary>
		public static Colord Goldenrod
		{
			get { return new Colord(0.854901960784314, 0.647058823529412, 0.125490196078431, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:128 Blue:128 Alpha:255.
		/// </summary>
		public static Colord Gray
		{
			get { return new Colord(0.501960784313725, 0.501960784313725, 0.501960784313725, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:128 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Green
		{
			get { return new Colord(0, 0.501960784313725, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:173 Green:255 Blue:47 Alpha:255.
		/// </summary>
		public static Colord GreenYellow
		{
			get { return new Colord(0.67843137254902, 1, 0.184313725490196, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:255 Blue:240 Alpha:255.
		/// </summary>
		public static Colord Honeydew
		{
			get { return new Colord(0.941176470588235, 1, 0.941176470588235, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:105 Blue:180 Alpha:255.
		/// </summary>
		public static Colord HotPink
		{
			get { return new Colord(1, 0.411764705882353, 0.705882352941177, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:205 Green:92 Blue:92 Alpha:255.
		/// </summary>
		public static Colord IndianRed
		{
			get { return new Colord(0.803921568627451, 0.36078431372549, 0.36078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:75 Green:0 Blue:130 Alpha:255.
		/// </summary>
		public static Colord Indigo
		{
			get { return new Colord(0.294117647058824, 0, 0.509803921568627, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:240 Alpha:255.
		/// </summary>
		public static Colord Ivory
		{
			get { return new Colord(1, 1, 0.941176470588235, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:230 Blue:140 Alpha:255.
		/// </summary>
		public static Colord Khaki
		{
			get { return new Colord(0.941176470588235, 0.901960784313726, 0.549019607843137, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:230 Green:230 Blue:250 Alpha:255.
		/// </summary>
		public static Colord Lavender
		{
			get { return new Colord(0.901960784313726, 0.901960784313726, 0.980392156862745, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:240 Blue:245 Alpha:255.
		/// </summary>
		public static Colord LavenderBlush
		{
			get { return new Colord(1, 0.941176470588235, 0.96078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:124 Green:252 Blue:0 Alpha:255.
		/// </summary>
		public static Colord LawnGreen
		{
			get { return new Colord(0.486274509803922, 0.988235294117647, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:250 Blue:205 Alpha:255.
		/// </summary>
		public static Colord LemonChiffon
		{
			get { return new Colord(1, 0.980392156862745, 0.803921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:173 Green:216 Blue:230 Alpha:255.
		/// </summary>
		public static Colord LightBlue
		{
			get { return new Colord(0.67843137254902, 0.847058823529412, 0.901960784313726, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:240 Green:128 Blue:128 Alpha:255.
		/// </summary>
		public static Colord LightCoral
		{
			get { return new Colord(0.941176470588235, 0.501960784313725, 0.501960784313725, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:224 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colord LightCyan
		{
			get { return new Colord(0.87843137254902, 1, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:250 Blue:210 Alpha:255.
		/// </summary>
		public static Colord LightGoldenrodYellow
		{
			get { return new Colord(0.980392156862745, 0.980392156862745, 0.823529411764706, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:144 Green:238 Blue:144 Alpha:255.
		/// </summary>
		public static Colord LightGreen
		{
			get { return new Colord(0.564705882352941, 0.933333333333333, 0.564705882352941, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:211 Green:211 Blue:211 Alpha:255.
		/// </summary>
		public static Colord LightGray
		{
			get { return new Colord(0.827450980392157, 0.827450980392157, 0.827450980392157, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:182 Blue:193 Alpha:255.
		/// </summary>
		public static Colord LightPink
		{
			get { return new Colord(1, 0.713725490196078, 0.756862745098039, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:160 Blue:122 Alpha:255.
		/// </summary>
		public static Colord LightSalmon
		{
			get { return new Colord(1, 0.627450980392157, 0.47843137254902, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:32 Green:178 Blue:170 Alpha:255.
		/// </summary>
		public static Colord LightSeaGreen
		{
			get { return new Colord(0.125490196078431, 0.698039215686274, 0.666666666666667, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:135 Green:206 Blue:250 Alpha:255.
		/// </summary>
		public static Colord LightSkyBlue
		{
			get { return new Colord(0.529411764705882, 0.807843137254902, 0.980392156862745, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:119 Green:136 Blue:153 Alpha:255.
		/// </summary>
		public static Colord LightSlateGray
		{
			get { return new Colord(0.466666666666667, 0.533333333333333, 0.6, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:176 Green:196 Blue:222 Alpha:255.
		/// </summary>
		public static Colord LightSteelBlue
		{
			get { return new Colord(0.690196078431373, 0.768627450980392, 0.870588235294118, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:224 Alpha:255.
		/// </summary>
		public static Colord LightYellow
		{
			get { return new Colord(1, 1, 0.87843137254902, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Lime
		{
			get { return new Colord(0, 1, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:50 Green:205 Blue:50 Alpha:255.
		/// </summary>
		public static Colord LimeGreen
		{
			get { return new Colord(0.196078431372549, 0.803921568627451, 0.196078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:240 Blue:230 Alpha:255.
		/// </summary>
		public static Colord Linen
		{
			get { return new Colord(0.980392156862745, 0.941176470588235, 0.901960784313726, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:0 Blue:255 Alpha:255.
		/// </summary>
		public static Colord Magenta
		{
			get { return new Colord(1, 0, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Maroon
		{
			get { return new Colord(0.501960784313725, 0, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:102 Green:205 Blue:170 Alpha:255.
		/// </summary>
		public static Colord MediumAquamarine
		{
			get { return new Colord(0.4, 0.803921568627451, 0.666666666666667, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:205 Alpha:255.
		/// </summary>
		public static Colord MediumBlue
		{
			get { return new Colord(0, 0, 0.803921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:186 Green:85 Blue:211 Alpha:255.
		/// </summary>
		public static Colord MediumOrchid
		{
			get { return new Colord(0.729411764705882, 0.333333333333333, 0.827450980392157, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:147 Green:112 Blue:219 Alpha:255.
		/// </summary>
		public static Colord MediumPurple
		{
			get { return new Colord(0.576470588235294, 0.43921568627451, 0.858823529411765, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:60 Green:179 Blue:113 Alpha:255.
		/// </summary>
		public static Colord MediumSeaGreen
		{
			get { return new Colord(0.235294117647059, 0.701960784313725, 0.443137254901961, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:123 Green:104 Blue:238 Alpha:255.
		/// </summary>
		public static Colord MediumSlateBlue
		{
			get { return new Colord(0.482352941176471, 0.407843137254902, 0.933333333333333, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:250 Blue:154 Alpha:255.
		/// </summary>
		public static Colord MediumSpringGreen
		{
			get { return new Colord(0, 0.980392156862745, 0.603921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:72 Green:209 Blue:204 Alpha:255.
		/// </summary>
		public static Colord MediumTurquoise
		{
			get { return new Colord(0.282352941176471, 0.819607843137255, 0.8, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:199 Green:21 Blue:133 Alpha:255.
		/// </summary>
		public static Colord MediumVioletRed
		{
			get { return new Colord(0.780392156862745, 0.0823529411764706, 0.52156862745098, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:25 Green:25 Blue:112 Alpha:255.
		/// </summary>
		public static Colord MidnightBlue
		{
			get { return new Colord(0.0980392156862745, 0.0980392156862745, 0.43921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:255 Blue:250 Alpha:255.
		/// </summary>
		public static Colord MintCream
		{
			get { return new Colord(0.96078431372549, 1, 0.980392156862745, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:228 Blue:225 Alpha:255.
		/// </summary>
		public static Colord MistyRose
		{
			get { return new Colord(1, 0.894117647058824, 0.882352941176471, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:228 Blue:181 Alpha:255.
		/// </summary>
		public static Colord Moccasin
		{
			get { return new Colord(1, 0.894117647058824, 0.709803921568627, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:222 Blue:173 Alpha:255.
		/// </summary>
		public static Colord NavajoWhite
		{
			get { return new Colord(1, 0.870588235294118, 0.67843137254902, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:0 Blue:128 Alpha:255.
		/// </summary>
		public static Colord Navy
		{
			get { return new Colord(0, 0, 0.501960784313725, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:253 Green:245 Blue:230 Alpha:255.
		/// </summary>
		public static Colord OldLace
		{
			get { return new Colord(0.992156862745098, 0.96078431372549, 0.901960784313726, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:128 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Olive
		{
			get { return new Colord(0.501960784313725, 0.501960784313725, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:107 Green:142 Blue:35 Alpha:255.
		/// </summary>
		public static Colord OliveDrab
		{
			get { return new Colord(0.419607843137255, 0.556862745098039, 0.137254901960784, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:165 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Orange
		{
			get { return new Colord(1, 0.647058823529412, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:69 Blue:0 Alpha:255.
		/// </summary>
		public static Colord OrangeRed
		{
			get { return new Colord(1, 0.270588235294118, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:218 Green:112 Blue:214 Alpha:255.
		/// </summary>
		public static Colord Orchid
		{
			get { return new Colord(0.854901960784314, 0.43921568627451, 0.83921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:238 Green:232 Blue:170 Alpha:255.
		/// </summary>
		public static Colord PaleGoldenrod
		{
			get { return new Colord(0.933333333333333, 0.909803921568627, 0.666666666666667, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:152 Green:251 Blue:152 Alpha:255.
		/// </summary>
		public static Colord PaleGreen
		{
			get { return new Colord(0.596078431372549, 0.984313725490196, 0.596078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:175 Green:238 Blue:238 Alpha:255.
		/// </summary>
		public static Colord PaleTurquoise
		{
			get { return new Colord(0.686274509803922, 0.933333333333333, 0.933333333333333, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:219 Green:112 Blue:147 Alpha:255.
		/// </summary>
		public static Colord PaleVioletRed
		{
			get { return new Colord(0.858823529411765, 0.43921568627451, 0.576470588235294, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:239 Blue:213 Alpha:255.
		/// </summary>
		public static Colord PapayaWhip
		{
			get { return new Colord(1, 0.937254901960784, 0.835294117647059, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:218 Blue:185 Alpha:255.
		/// </summary>
		public static Colord PeachPuff
		{
			get { return new Colord(1, 0.854901960784314, 0.725490196078431, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:205 Green:133 Blue:63 Alpha:255.
		/// </summary>
		public static Colord Peru
		{
			get { return new Colord(0.803921568627451, 0.52156862745098, 0.247058823529412, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:192 Blue:203 Alpha:255.
		/// </summary>
		public static Colord Pink
		{
			get { return new Colord(1, 0.752941176470588, 0.796078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:221 Green:160 Blue:221 Alpha:255.
		/// </summary>
		public static Colord Plum
		{
			get { return new Colord(0.866666666666667, 0.627450980392157, 0.866666666666667, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:176 Green:224 Blue:230 Alpha:255.
		/// </summary>
		public static Colord PowderBlue
		{
			get { return new Colord(0.690196078431373, 0.87843137254902, 0.901960784313726, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:128 Green:0 Blue:128 Alpha:255.
		/// </summary>
		public static Colord Purple
		{
			get { return new Colord(0.501960784313725, 0, 0.501960784313725, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:0 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Red
		{
			get { return new Colord(1, 0, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:188 Green:143 Blue:143 Alpha:255.
		/// </summary>
		public static Colord RosyBrown
		{
			get { return new Colord(0.737254901960784, 0.56078431372549, 0.56078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:65 Green:105 Blue:225 Alpha:255.
		/// </summary>
		public static Colord RoyalBlue
		{
			get { return new Colord(0.254901960784314, 0.411764705882353, 0.882352941176471, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:139 Green:69 Blue:19 Alpha:255.
		/// </summary>
		public static Colord SaddleBrown
		{
			get { return new Colord(0.545098039215686, 0.270588235294118, 0.0745098039215686, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:250 Green:128 Blue:114 Alpha:255.
		/// </summary>
		public static Colord Salmon
		{
			get { return new Colord(0.980392156862745, 0.501960784313725, 0.447058823529412, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:244 Green:164 Blue:96 Alpha:255.
		/// </summary>
		public static Colord SandyBrown
		{
			get { return new Colord(0.956862745098039, 0.643137254901961, 0.376470588235294, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:46 Green:139 Blue:87 Alpha:255.
		/// </summary>
		public static Colord SeaGreen
		{
			get { return new Colord(0.180392156862745, 0.545098039215686, 0.341176470588235, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:245 Blue:238 Alpha:255.
		/// </summary>
		public static Colord SeaShell
		{
			get { return new Colord(1, 0.96078431372549, 0.933333333333333, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:160 Green:82 Blue:45 Alpha:255.
		/// </summary>
		public static Colord Sienna
		{
			get { return new Colord(0.627450980392157, 0.32156862745098, 0.176470588235294, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:192 Green:192 Blue:192 Alpha:255.
		/// </summary>
		public static Colord Silver
		{
			get { return new Colord(0.752941176470588, 0.752941176470588, 0.752941176470588, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:135 Green:206 Blue:235 Alpha:255.
		/// </summary>
		public static Colord SkyBlue
		{
			get { return new Colord(0.529411764705882, 0.807843137254902, 0.92156862745098, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:106 Green:90 Blue:205 Alpha:255.
		/// </summary>
		public static Colord SlateBlue
		{
			get { return new Colord(0.415686274509804, 0.352941176470588, 0.803921568627451, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:112 Green:128 Blue:144 Alpha:255.
		/// </summary>
		public static Colord SlateGray
		{
			get { return new Colord(0.43921568627451, 0.501960784313725, 0.564705882352941, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:250 Blue:250 Alpha:255.
		/// </summary>
		public static Colord Snow
		{
			get { return new Colord(1, 0.980392156862745, 0.980392156862745, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:255 Blue:127 Alpha:255.
		/// </summary>
		public static Colord SpringGreen
		{
			get { return new Colord(0, 1, 0.498039215686275, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:70 Green:130 Blue:180 Alpha:255.
		/// </summary>
		public static Colord SteelBlue
		{
			get { return new Colord(0.274509803921569, 0.509803921568627, 0.705882352941177, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:210 Green:180 Blue:140 Alpha:255.
		/// </summary>
		public static Colord Tan
		{
			get { return new Colord(0.823529411764706, 0.705882352941177, 0.549019607843137, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:0 Green:128 Blue:128 Alpha:255.
		/// </summary>
		public static Colord Teal
		{
			get { return new Colord(0, 0.501960784313725, 0.501960784313725, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:216 Green:191 Blue:216 Alpha:255.
		/// </summary>
		public static Colord Thistle
		{
			get { return new Colord(0.847058823529412, 0.749019607843137, 0.847058823529412, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:99 Blue:71 Alpha:255.
		/// </summary>
		public static Colord Tomato
		{
			get { return new Colord(1, 0.388235294117647, 0.27843137254902, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:64 Green:224 Blue:208 Alpha:255.
		/// </summary>
		public static Colord Turquoise
		{
			get { return new Colord(0.250980392156863, 0.87843137254902, 0.815686274509804, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:238 Green:130 Blue:238 Alpha:255.
		/// </summary>
		public static Colord Violet
		{
			get { return new Colord(0.933333333333333, 0.509803921568627, 0.933333333333333, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:222 Blue:179 Alpha:255.
		/// </summary>
		public static Colord Wheat
		{
			get { return new Colord(0.96078431372549, 0.870588235294118, 0.701960784313725, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:255 Alpha:255.
		/// </summary>
		public static Colord White
		{
			get { return new Colord(1, 1, 1, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:245 Green:245 Blue:245 Alpha:255.
		/// </summary>
		public static Colord WhiteSmoke
		{
			get { return new Colord(0.96078431372549, 0.96078431372549, 0.96078431372549, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:255 Green:255 Blue:0 Alpha:255.
		/// </summary>
		public static Colord Yellow
		{
			get { return new Colord(1, 1, 0, 1); }
		}
		/// <summary>
		/// Gets a color with the value Red:154 Green:205 Blue:50 Alpha:255.
		/// </summary>
		public static Colord YellowGreen
		{
			get { return new Colord(0.603921568627451, 0.803921568627451, 0.196078431372549, 1); }
		}
		#endregion
		#region Fields
		/// <summary>
		/// The color's red component.
		/// </summary>
		public readonly double R;
		/// <summary>
		/// The color's green component.
		/// </summary>
		public readonly double G;
		/// <summary>
		/// The color's blue component.
		/// </summary>
		public readonly double B;
		/// <summary>
		/// The color's alpha component.
		/// </summary>
		public readonly double A;
		#endregion
		#region Properties
		/// <summary>
		/// Returns the indexed component of this color.
		/// </summary>
		/// <param name="index">The index of the component.</param>
		/// <returns>The value of the indexed component.</returns>
		public double this[int index]
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
						throw new IndexOutOfRangeException("Indices for Colord run from 0 to 3, inclusive.");
				}
			}
		}
		public double[] ToArray()
		{
			return new double[]
			{
				R, G, B, A
			};
		}
		#region Swizzles
		/// <summary>
		/// Returns the color (R, R, R, R).
		/// </summary>
		public Colord RRRR
		{
			get
			{
				return new Colord(R, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, R, G).
		/// </summary>
		public Colord RRRG
		{
			get
			{
				return new Colord(R, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, R, B).
		/// </summary>
		public Colord RRRB
		{
			get
			{
				return new Colord(R, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, R, A).
		/// </summary>
		public Colord RRRA
		{
			get
			{
				return new Colord(R, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, R).
		/// </summary>
		public Colord RRGR
		{
			get
			{
				return new Colord(R, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, G).
		/// </summary>
		public Colord RRGG
		{
			get
			{
				return new Colord(R, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, B).
		/// </summary>
		public Colord RRGB
		{
			get
			{
				return new Colord(R, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, G, A).
		/// </summary>
		public Colord RRGA
		{
			get
			{
				return new Colord(R, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, R).
		/// </summary>
		public Colord RRBR
		{
			get
			{
				return new Colord(R, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, G).
		/// </summary>
		public Colord RRBG
		{
			get
			{
				return new Colord(R, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, B).
		/// </summary>
		public Colord RRBB
		{
			get
			{
				return new Colord(R, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, B, A).
		/// </summary>
		public Colord RRBA
		{
			get
			{
				return new Colord(R, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, R).
		/// </summary>
		public Colord RRAR
		{
			get
			{
				return new Colord(R, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, G).
		/// </summary>
		public Colord RRAG
		{
			get
			{
				return new Colord(R, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, B).
		/// </summary>
		public Colord RRAB
		{
			get
			{
				return new Colord(R, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, R, A, A).
		/// </summary>
		public Colord RRAA
		{
			get
			{
				return new Colord(R, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, R).
		/// </summary>
		public Colord RGRR
		{
			get
			{
				return new Colord(R, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, G).
		/// </summary>
		public Colord RGRG
		{
			get
			{
				return new Colord(R, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, B).
		/// </summary>
		public Colord RGRB
		{
			get
			{
				return new Colord(R, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, R, A).
		/// </summary>
		public Colord RGRA
		{
			get
			{
				return new Colord(R, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, R).
		/// </summary>
		public Colord RGGR
		{
			get
			{
				return new Colord(R, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, G).
		/// </summary>
		public Colord RGGG
		{
			get
			{
				return new Colord(R, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, B).
		/// </summary>
		public Colord RGGB
		{
			get
			{
				return new Colord(R, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, G, A).
		/// </summary>
		public Colord RGGA
		{
			get
			{
				return new Colord(R, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, R).
		/// </summary>
		public Colord RGBR
		{
			get
			{
				return new Colord(R, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, G).
		/// </summary>
		public Colord RGBG
		{
			get
			{
				return new Colord(R, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, B).
		/// </summary>
		public Colord RGBB
		{
			get
			{
				return new Colord(R, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, B, A).
		/// </summary>
		public Colord RGBA
		{
			get
			{
				return new Colord(R, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, R).
		/// </summary>
		public Colord RGAR
		{
			get
			{
				return new Colord(R, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, G).
		/// </summary>
		public Colord RGAG
		{
			get
			{
				return new Colord(R, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, B).
		/// </summary>
		public Colord RGAB
		{
			get
			{
				return new Colord(R, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, G, A, A).
		/// </summary>
		public Colord RGAA
		{
			get
			{
				return new Colord(R, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, R).
		/// </summary>
		public Colord RBRR
		{
			get
			{
				return new Colord(R, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, G).
		/// </summary>
		public Colord RBRG
		{
			get
			{
				return new Colord(R, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, B).
		/// </summary>
		public Colord RBRB
		{
			get
			{
				return new Colord(R, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, R, A).
		/// </summary>
		public Colord RBRA
		{
			get
			{
				return new Colord(R, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, R).
		/// </summary>
		public Colord RBGR
		{
			get
			{
				return new Colord(R, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, G).
		/// </summary>
		public Colord RBGG
		{
			get
			{
				return new Colord(R, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, B).
		/// </summary>
		public Colord RBGB
		{
			get
			{
				return new Colord(R, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, G, A).
		/// </summary>
		public Colord RBGA
		{
			get
			{
				return new Colord(R, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, R).
		/// </summary>
		public Colord RBBR
		{
			get
			{
				return new Colord(R, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, G).
		/// </summary>
		public Colord RBBG
		{
			get
			{
				return new Colord(R, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, B).
		/// </summary>
		public Colord RBBB
		{
			get
			{
				return new Colord(R, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, B, A).
		/// </summary>
		public Colord RBBA
		{
			get
			{
				return new Colord(R, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, R).
		/// </summary>
		public Colord RBAR
		{
			get
			{
				return new Colord(R, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, G).
		/// </summary>
		public Colord RBAG
		{
			get
			{
				return new Colord(R, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, B).
		/// </summary>
		public Colord RBAB
		{
			get
			{
				return new Colord(R, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, B, A, A).
		/// </summary>
		public Colord RBAA
		{
			get
			{
				return new Colord(R, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, R).
		/// </summary>
		public Colord RARR
		{
			get
			{
				return new Colord(R, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, G).
		/// </summary>
		public Colord RARG
		{
			get
			{
				return new Colord(R, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, B).
		/// </summary>
		public Colord RARB
		{
			get
			{
				return new Colord(R, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, R, A).
		/// </summary>
		public Colord RARA
		{
			get
			{
				return new Colord(R, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, R).
		/// </summary>
		public Colord RAGR
		{
			get
			{
				return new Colord(R, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, G).
		/// </summary>
		public Colord RAGG
		{
			get
			{
				return new Colord(R, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, B).
		/// </summary>
		public Colord RAGB
		{
			get
			{
				return new Colord(R, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, G, A).
		/// </summary>
		public Colord RAGA
		{
			get
			{
				return new Colord(R, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, R).
		/// </summary>
		public Colord RABR
		{
			get
			{
				return new Colord(R, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, G).
		/// </summary>
		public Colord RABG
		{
			get
			{
				return new Colord(R, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, B).
		/// </summary>
		public Colord RABB
		{
			get
			{
				return new Colord(R, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, B, A).
		/// </summary>
		public Colord RABA
		{
			get
			{
				return new Colord(R, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, R).
		/// </summary>
		public Colord RAAR
		{
			get
			{
				return new Colord(R, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, G).
		/// </summary>
		public Colord RAAG
		{
			get
			{
				return new Colord(R, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, B).
		/// </summary>
		public Colord RAAB
		{
			get
			{
				return new Colord(R, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (R, A, A, A).
		/// </summary>
		public Colord RAAA
		{
			get
			{
				return new Colord(R, A, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, R).
		/// </summary>
		public Colord GRRR
		{
			get
			{
				return new Colord(G, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, G).
		/// </summary>
		public Colord GRRG
		{
			get
			{
				return new Colord(G, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, B).
		/// </summary>
		public Colord GRRB
		{
			get
			{
				return new Colord(G, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, R, A).
		/// </summary>
		public Colord GRRA
		{
			get
			{
				return new Colord(G, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, R).
		/// </summary>
		public Colord GRGR
		{
			get
			{
				return new Colord(G, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, G).
		/// </summary>
		public Colord GRGG
		{
			get
			{
				return new Colord(G, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, B).
		/// </summary>
		public Colord GRGB
		{
			get
			{
				return new Colord(G, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, G, A).
		/// </summary>
		public Colord GRGA
		{
			get
			{
				return new Colord(G, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, R).
		/// </summary>
		public Colord GRBR
		{
			get
			{
				return new Colord(G, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, G).
		/// </summary>
		public Colord GRBG
		{
			get
			{
				return new Colord(G, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, B).
		/// </summary>
		public Colord GRBB
		{
			get
			{
				return new Colord(G, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, B, A).
		/// </summary>
		public Colord GRBA
		{
			get
			{
				return new Colord(G, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, R).
		/// </summary>
		public Colord GRAR
		{
			get
			{
				return new Colord(G, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, G).
		/// </summary>
		public Colord GRAG
		{
			get
			{
				return new Colord(G, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, B).
		/// </summary>
		public Colord GRAB
		{
			get
			{
				return new Colord(G, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, R, A, A).
		/// </summary>
		public Colord GRAA
		{
			get
			{
				return new Colord(G, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, R).
		/// </summary>
		public Colord GGRR
		{
			get
			{
				return new Colord(G, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, G).
		/// </summary>
		public Colord GGRG
		{
			get
			{
				return new Colord(G, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, B).
		/// </summary>
		public Colord GGRB
		{
			get
			{
				return new Colord(G, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, R, A).
		/// </summary>
		public Colord GGRA
		{
			get
			{
				return new Colord(G, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, R).
		/// </summary>
		public Colord GGGR
		{
			get
			{
				return new Colord(G, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, G).
		/// </summary>
		public Colord GGGG
		{
			get
			{
				return new Colord(G, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, B).
		/// </summary>
		public Colord GGGB
		{
			get
			{
				return new Colord(G, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, G, A).
		/// </summary>
		public Colord GGGA
		{
			get
			{
				return new Colord(G, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, R).
		/// </summary>
		public Colord GGBR
		{
			get
			{
				return new Colord(G, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, G).
		/// </summary>
		public Colord GGBG
		{
			get
			{
				return new Colord(G, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, B).
		/// </summary>
		public Colord GGBB
		{
			get
			{
				return new Colord(G, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, B, A).
		/// </summary>
		public Colord GGBA
		{
			get
			{
				return new Colord(G, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, R).
		/// </summary>
		public Colord GGAR
		{
			get
			{
				return new Colord(G, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, G).
		/// </summary>
		public Colord GGAG
		{
			get
			{
				return new Colord(G, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, B).
		/// </summary>
		public Colord GGAB
		{
			get
			{
				return new Colord(G, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, G, A, A).
		/// </summary>
		public Colord GGAA
		{
			get
			{
				return new Colord(G, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, R).
		/// </summary>
		public Colord GBRR
		{
			get
			{
				return new Colord(G, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, G).
		/// </summary>
		public Colord GBRG
		{
			get
			{
				return new Colord(G, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, B).
		/// </summary>
		public Colord GBRB
		{
			get
			{
				return new Colord(G, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, R, A).
		/// </summary>
		public Colord GBRA
		{
			get
			{
				return new Colord(G, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, R).
		/// </summary>
		public Colord GBGR
		{
			get
			{
				return new Colord(G, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, G).
		/// </summary>
		public Colord GBGG
		{
			get
			{
				return new Colord(G, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, B).
		/// </summary>
		public Colord GBGB
		{
			get
			{
				return new Colord(G, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, G, A).
		/// </summary>
		public Colord GBGA
		{
			get
			{
				return new Colord(G, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, R).
		/// </summary>
		public Colord GBBR
		{
			get
			{
				return new Colord(G, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, G).
		/// </summary>
		public Colord GBBG
		{
			get
			{
				return new Colord(G, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, B).
		/// </summary>
		public Colord GBBB
		{
			get
			{
				return new Colord(G, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, B, A).
		/// </summary>
		public Colord GBBA
		{
			get
			{
				return new Colord(G, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, R).
		/// </summary>
		public Colord GBAR
		{
			get
			{
				return new Colord(G, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, G).
		/// </summary>
		public Colord GBAG
		{
			get
			{
				return new Colord(G, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, B).
		/// </summary>
		public Colord GBAB
		{
			get
			{
				return new Colord(G, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, B, A, A).
		/// </summary>
		public Colord GBAA
		{
			get
			{
				return new Colord(G, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, R).
		/// </summary>
		public Colord GARR
		{
			get
			{
				return new Colord(G, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, G).
		/// </summary>
		public Colord GARG
		{
			get
			{
				return new Colord(G, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, B).
		/// </summary>
		public Colord GARB
		{
			get
			{
				return new Colord(G, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, R, A).
		/// </summary>
		public Colord GARA
		{
			get
			{
				return new Colord(G, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, R).
		/// </summary>
		public Colord GAGR
		{
			get
			{
				return new Colord(G, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, G).
		/// </summary>
		public Colord GAGG
		{
			get
			{
				return new Colord(G, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, B).
		/// </summary>
		public Colord GAGB
		{
			get
			{
				return new Colord(G, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, G, A).
		/// </summary>
		public Colord GAGA
		{
			get
			{
				return new Colord(G, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, R).
		/// </summary>
		public Colord GABR
		{
			get
			{
				return new Colord(G, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, G).
		/// </summary>
		public Colord GABG
		{
			get
			{
				return new Colord(G, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, B).
		/// </summary>
		public Colord GABB
		{
			get
			{
				return new Colord(G, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, B, A).
		/// </summary>
		public Colord GABA
		{
			get
			{
				return new Colord(G, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, R).
		/// </summary>
		public Colord GAAR
		{
			get
			{
				return new Colord(G, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, G).
		/// </summary>
		public Colord GAAG
		{
			get
			{
				return new Colord(G, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, B).
		/// </summary>
		public Colord GAAB
		{
			get
			{
				return new Colord(G, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (G, A, A, A).
		/// </summary>
		public Colord GAAA
		{
			get
			{
				return new Colord(G, A, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, R).
		/// </summary>
		public Colord BRRR
		{
			get
			{
				return new Colord(B, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, G).
		/// </summary>
		public Colord BRRG
		{
			get
			{
				return new Colord(B, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, B).
		/// </summary>
		public Colord BRRB
		{
			get
			{
				return new Colord(B, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, R, A).
		/// </summary>
		public Colord BRRA
		{
			get
			{
				return new Colord(B, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, R).
		/// </summary>
		public Colord BRGR
		{
			get
			{
				return new Colord(B, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, G).
		/// </summary>
		public Colord BRGG
		{
			get
			{
				return new Colord(B, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, B).
		/// </summary>
		public Colord BRGB
		{
			get
			{
				return new Colord(B, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, G, A).
		/// </summary>
		public Colord BRGA
		{
			get
			{
				return new Colord(B, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, R).
		/// </summary>
		public Colord BRBR
		{
			get
			{
				return new Colord(B, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, G).
		/// </summary>
		public Colord BRBG
		{
			get
			{
				return new Colord(B, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, B).
		/// </summary>
		public Colord BRBB
		{
			get
			{
				return new Colord(B, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, B, A).
		/// </summary>
		public Colord BRBA
		{
			get
			{
				return new Colord(B, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, R).
		/// </summary>
		public Colord BRAR
		{
			get
			{
				return new Colord(B, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, G).
		/// </summary>
		public Colord BRAG
		{
			get
			{
				return new Colord(B, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, B).
		/// </summary>
		public Colord BRAB
		{
			get
			{
				return new Colord(B, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, R, A, A).
		/// </summary>
		public Colord BRAA
		{
			get
			{
				return new Colord(B, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, R).
		/// </summary>
		public Colord BGRR
		{
			get
			{
				return new Colord(B, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, G).
		/// </summary>
		public Colord BGRG
		{
			get
			{
				return new Colord(B, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, B).
		/// </summary>
		public Colord BGRB
		{
			get
			{
				return new Colord(B, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, R, A).
		/// </summary>
		public Colord BGRA
		{
			get
			{
				return new Colord(B, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, R).
		/// </summary>
		public Colord BGGR
		{
			get
			{
				return new Colord(B, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, G).
		/// </summary>
		public Colord BGGG
		{
			get
			{
				return new Colord(B, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, B).
		/// </summary>
		public Colord BGGB
		{
			get
			{
				return new Colord(B, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, G, A).
		/// </summary>
		public Colord BGGA
		{
			get
			{
				return new Colord(B, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, R).
		/// </summary>
		public Colord BGBR
		{
			get
			{
				return new Colord(B, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, G).
		/// </summary>
		public Colord BGBG
		{
			get
			{
				return new Colord(B, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, B).
		/// </summary>
		public Colord BGBB
		{
			get
			{
				return new Colord(B, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, B, A).
		/// </summary>
		public Colord BGBA
		{
			get
			{
				return new Colord(B, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, R).
		/// </summary>
		public Colord BGAR
		{
			get
			{
				return new Colord(B, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, G).
		/// </summary>
		public Colord BGAG
		{
			get
			{
				return new Colord(B, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, B).
		/// </summary>
		public Colord BGAB
		{
			get
			{
				return new Colord(B, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, G, A, A).
		/// </summary>
		public Colord BGAA
		{
			get
			{
				return new Colord(B, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, R).
		/// </summary>
		public Colord BBRR
		{
			get
			{
				return new Colord(B, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, G).
		/// </summary>
		public Colord BBRG
		{
			get
			{
				return new Colord(B, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, B).
		/// </summary>
		public Colord BBRB
		{
			get
			{
				return new Colord(B, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, R, A).
		/// </summary>
		public Colord BBRA
		{
			get
			{
				return new Colord(B, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, R).
		/// </summary>
		public Colord BBGR
		{
			get
			{
				return new Colord(B, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, G).
		/// </summary>
		public Colord BBGG
		{
			get
			{
				return new Colord(B, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, B).
		/// </summary>
		public Colord BBGB
		{
			get
			{
				return new Colord(B, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, G, A).
		/// </summary>
		public Colord BBGA
		{
			get
			{
				return new Colord(B, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, R).
		/// </summary>
		public Colord BBBR
		{
			get
			{
				return new Colord(B, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, G).
		/// </summary>
		public Colord BBBG
		{
			get
			{
				return new Colord(B, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, B).
		/// </summary>
		public Colord BBBB
		{
			get
			{
				return new Colord(B, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, B, A).
		/// </summary>
		public Colord BBBA
		{
			get
			{
				return new Colord(B, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, R).
		/// </summary>
		public Colord BBAR
		{
			get
			{
				return new Colord(B, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, G).
		/// </summary>
		public Colord BBAG
		{
			get
			{
				return new Colord(B, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, B).
		/// </summary>
		public Colord BBAB
		{
			get
			{
				return new Colord(B, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, B, A, A).
		/// </summary>
		public Colord BBAA
		{
			get
			{
				return new Colord(B, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, R).
		/// </summary>
		public Colord BARR
		{
			get
			{
				return new Colord(B, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, G).
		/// </summary>
		public Colord BARG
		{
			get
			{
				return new Colord(B, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, B).
		/// </summary>
		public Colord BARB
		{
			get
			{
				return new Colord(B, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, R, A).
		/// </summary>
		public Colord BARA
		{
			get
			{
				return new Colord(B, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, R).
		/// </summary>
		public Colord BAGR
		{
			get
			{
				return new Colord(B, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, G).
		/// </summary>
		public Colord BAGG
		{
			get
			{
				return new Colord(B, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, B).
		/// </summary>
		public Colord BAGB
		{
			get
			{
				return new Colord(B, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, G, A).
		/// </summary>
		public Colord BAGA
		{
			get
			{
				return new Colord(B, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, R).
		/// </summary>
		public Colord BABR
		{
			get
			{
				return new Colord(B, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, G).
		/// </summary>
		public Colord BABG
		{
			get
			{
				return new Colord(B, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, B).
		/// </summary>
		public Colord BABB
		{
			get
			{
				return new Colord(B, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, B, A).
		/// </summary>
		public Colord BABA
		{
			get
			{
				return new Colord(B, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, R).
		/// </summary>
		public Colord BAAR
		{
			get
			{
				return new Colord(B, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, G).
		/// </summary>
		public Colord BAAG
		{
			get
			{
				return new Colord(B, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, B).
		/// </summary>
		public Colord BAAB
		{
			get
			{
				return new Colord(B, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (B, A, A, A).
		/// </summary>
		public Colord BAAA
		{
			get
			{
				return new Colord(B, A, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, R).
		/// </summary>
		public Colord ARRR
		{
			get
			{
				return new Colord(A, R, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, G).
		/// </summary>
		public Colord ARRG
		{
			get
			{
				return new Colord(A, R, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, B).
		/// </summary>
		public Colord ARRB
		{
			get
			{
				return new Colord(A, R, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, R, A).
		/// </summary>
		public Colord ARRA
		{
			get
			{
				return new Colord(A, R, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, R).
		/// </summary>
		public Colord ARGR
		{
			get
			{
				return new Colord(A, R, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, G).
		/// </summary>
		public Colord ARGG
		{
			get
			{
				return new Colord(A, R, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, B).
		/// </summary>
		public Colord ARGB
		{
			get
			{
				return new Colord(A, R, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, G, A).
		/// </summary>
		public Colord ARGA
		{
			get
			{
				return new Colord(A, R, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, R).
		/// </summary>
		public Colord ARBR
		{
			get
			{
				return new Colord(A, R, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, G).
		/// </summary>
		public Colord ARBG
		{
			get
			{
				return new Colord(A, R, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, B).
		/// </summary>
		public Colord ARBB
		{
			get
			{
				return new Colord(A, R, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, B, A).
		/// </summary>
		public Colord ARBA
		{
			get
			{
				return new Colord(A, R, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, R).
		/// </summary>
		public Colord ARAR
		{
			get
			{
				return new Colord(A, R, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, G).
		/// </summary>
		public Colord ARAG
		{
			get
			{
				return new Colord(A, R, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, B).
		/// </summary>
		public Colord ARAB
		{
			get
			{
				return new Colord(A, R, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, R, A, A).
		/// </summary>
		public Colord ARAA
		{
			get
			{
				return new Colord(A, R, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, R).
		/// </summary>
		public Colord AGRR
		{
			get
			{
				return new Colord(A, G, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, G).
		/// </summary>
		public Colord AGRG
		{
			get
			{
				return new Colord(A, G, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, B).
		/// </summary>
		public Colord AGRB
		{
			get
			{
				return new Colord(A, G, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, R, A).
		/// </summary>
		public Colord AGRA
		{
			get
			{
				return new Colord(A, G, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, R).
		/// </summary>
		public Colord AGGR
		{
			get
			{
				return new Colord(A, G, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, G).
		/// </summary>
		public Colord AGGG
		{
			get
			{
				return new Colord(A, G, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, B).
		/// </summary>
		public Colord AGGB
		{
			get
			{
				return new Colord(A, G, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, G, A).
		/// </summary>
		public Colord AGGA
		{
			get
			{
				return new Colord(A, G, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, R).
		/// </summary>
		public Colord AGBR
		{
			get
			{
				return new Colord(A, G, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, G).
		/// </summary>
		public Colord AGBG
		{
			get
			{
				return new Colord(A, G, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, B).
		/// </summary>
		public Colord AGBB
		{
			get
			{
				return new Colord(A, G, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, B, A).
		/// </summary>
		public Colord AGBA
		{
			get
			{
				return new Colord(A, G, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, R).
		/// </summary>
		public Colord AGAR
		{
			get
			{
				return new Colord(A, G, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, G).
		/// </summary>
		public Colord AGAG
		{
			get
			{
				return new Colord(A, G, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, B).
		/// </summary>
		public Colord AGAB
		{
			get
			{
				return new Colord(A, G, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, G, A, A).
		/// </summary>
		public Colord AGAA
		{
			get
			{
				return new Colord(A, G, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, R).
		/// </summary>
		public Colord ABRR
		{
			get
			{
				return new Colord(A, B, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, G).
		/// </summary>
		public Colord ABRG
		{
			get
			{
				return new Colord(A, B, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, B).
		/// </summary>
		public Colord ABRB
		{
			get
			{
				return new Colord(A, B, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, R, A).
		/// </summary>
		public Colord ABRA
		{
			get
			{
				return new Colord(A, B, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, R).
		/// </summary>
		public Colord ABGR
		{
			get
			{
				return new Colord(A, B, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, G).
		/// </summary>
		public Colord ABGG
		{
			get
			{
				return new Colord(A, B, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, B).
		/// </summary>
		public Colord ABGB
		{
			get
			{
				return new Colord(A, B, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, G, A).
		/// </summary>
		public Colord ABGA
		{
			get
			{
				return new Colord(A, B, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, R).
		/// </summary>
		public Colord ABBR
		{
			get
			{
				return new Colord(A, B, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, G).
		/// </summary>
		public Colord ABBG
		{
			get
			{
				return new Colord(A, B, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, B).
		/// </summary>
		public Colord ABBB
		{
			get
			{
				return new Colord(A, B, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, B, A).
		/// </summary>
		public Colord ABBA
		{
			get
			{
				return new Colord(A, B, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, R).
		/// </summary>
		public Colord ABAR
		{
			get
			{
				return new Colord(A, B, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, G).
		/// </summary>
		public Colord ABAG
		{
			get
			{
				return new Colord(A, B, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, B).
		/// </summary>
		public Colord ABAB
		{
			get
			{
				return new Colord(A, B, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, B, A, A).
		/// </summary>
		public Colord ABAA
		{
			get
			{
				return new Colord(A, B, A, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, R).
		/// </summary>
		public Colord AARR
		{
			get
			{
				return new Colord(A, A, R, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, G).
		/// </summary>
		public Colord AARG
		{
			get
			{
				return new Colord(A, A, R, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, B).
		/// </summary>
		public Colord AARB
		{
			get
			{
				return new Colord(A, A, R, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, R, A).
		/// </summary>
		public Colord AARA
		{
			get
			{
				return new Colord(A, A, R, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, R).
		/// </summary>
		public Colord AAGR
		{
			get
			{
				return new Colord(A, A, G, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, G).
		/// </summary>
		public Colord AAGG
		{
			get
			{
				return new Colord(A, A, G, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, B).
		/// </summary>
		public Colord AAGB
		{
			get
			{
				return new Colord(A, A, G, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, G, A).
		/// </summary>
		public Colord AAGA
		{
			get
			{
				return new Colord(A, A, G, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, R).
		/// </summary>
		public Colord AABR
		{
			get
			{
				return new Colord(A, A, B, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, G).
		/// </summary>
		public Colord AABG
		{
			get
			{
				return new Colord(A, A, B, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, B).
		/// </summary>
		public Colord AABB
		{
			get
			{
				return new Colord(A, A, B, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, B, A).
		/// </summary>
		public Colord AABA
		{
			get
			{
				return new Colord(A, A, B, A);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, R).
		/// </summary>
		public Colord AAAR
		{
			get
			{
				return new Colord(A, A, A, R);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, G).
		/// </summary>
		public Colord AAAG
		{
			get
			{
				return new Colord(A, A, A, G);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, B).
		/// </summary>
		public Colord AAAB
		{
			get
			{
				return new Colord(A, A, A, B);
			}
		}
		/// <summary>
		/// Returns the color (A, A, A, A).
		/// </summary>
		public Colord AAAA
		{
			get
			{
				return new Colord(A, A, A, A);
			}
		}
		#endregion
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Colord"/> using the specified values.
		/// </summary>
		/// <param name="red">Value for the red component of the color.</param>
		/// <param name="green">Value for the green component of the color.</param>
		/// <param name="blue">Value for the blue component of the color.</param>
		public Colord(double red, double green, double blue)
			: this(red, green, blue, 1)
		{
			Contract.Requires(0 <= red);
			Contract.Requires(0 <= green);
			Contract.Requires(0 <= blue);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Colord"/> using the specified values.
		/// </summary>
		/// <param name="red">Value for the red component of the color.</param>
		/// <param name="green">Value for the green component of the color.</param>
		/// <param name="blue">Value for the blue component of the color.</param>
		/// <param name="alpha">Value for the alpha component of the color.</param>
		public Colord(double red, double green, double blue, double alpha)
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
		/// Initializes a new instance of the <see cref="Colord"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		public Colord(double[] array)
			: this(array, 0)
		{
			Contract.Requires(array != null);
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Colord"/> using the specified array.
		/// </summary>
		/// <param name="array">Array of values for the vector.</param>
		/// <param name="offset">Offset to start copying values from.</param>
		public Colord(double[] array, int offset)
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
		public static Colord operator +(Colord value)
		{
			return value;
		}
		/// <summary>
		/// Adds two colors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Colord operator +(Colord left, Colord right)
		{
			return Color.Add(left, right);
		}
		/// <summary>
		/// Subtracts one color from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Colord operator -(Colord left, Colord right)
		{
			return Color.Subtract(left, right);
		}
		/// <summary>
		/// Returns the product of a color and scalar.
		/// </summary>
		/// <param name="left">The color to multiply.</param>
		/// <param name="right">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Colord operator *(Colord left, double right)
		{
			return Color.Multiply(left, right);
		}
		/// <summary>
		/// Returns the product of a scalar and color.
		/// </summary>
		/// <param name="left">The scalar to multiply.</param>
		/// <param name="right">The color to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Colord operator *(double left, Colord right)
		{
			return Color.Multiply(right, left);
		}
		/// <summary>
		/// Divides a color by a scalar and returns the result.
		/// </summary>
		/// <param name="left">The color to be divided (the dividend).</param>
		/// <param name="right">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Colord operator /(Colord left, double right)
		{
			return Color.Divide(left, right);
		}
		#endregion
		#region Conversions
		/// <summary>
		/// Defines an implicit conversion of a Colorf value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Colorf value)
		{
			return new Colord((double)value.R, (double)value.G, (double)value.B, (double)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Colord value to a Vector4d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4d.</param>
		/// <returns>A Vector4d that has all components equal to value.</returns>
		public static implicit operator Vector4d(Colord value)
		{
			return new Vector4d((double)value.R, (double)value.G, (double)value.B, (double)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4d value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4d value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4f.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4f.</param>
		/// <returns>A Vector4f that has all components equal to value.</returns>
		public static explicit operator Vector4f(Colord value)
		{
			return new Vector4f((float)value.R, (float)value.G, (float)value.B, (float)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4f value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4f value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4h.</param>
		/// <returns>A Vector4h that has all components equal to value.</returns>
		public static explicit operator Vector4h(Colord value)
		{
			return new Vector4h((Half)value.R, (Half)value.G, (Half)value.B, (Half)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4h value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4h value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4ul.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4ul.</param>
		/// <returns>A Vector4ul that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4ul(Colord value)
		{
			return new Vector4ul((ulong)value.R, (ulong)value.G, (ulong)value.B, (ulong)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ul value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colord(Vector4ul value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4l.</param>
		/// <returns>A Vector4l that has all components equal to value.</returns>
		public static explicit operator Vector4l(Colord value)
		{
			return new Vector4l((long)value.R, (long)value.G, (long)value.B, (long)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4l value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4l value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4ui.</param>
		/// <returns>A Vector4ui that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4ui(Colord value)
		{
			return new Vector4ui((uint)value.R, (uint)value.G, (uint)value.B, (uint)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4ui value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colord(Vector4ui value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4i.</param>
		/// <returns>A Vector4i that has all components equal to value.</returns>
		public static explicit operator Vector4i(Colord value)
		{
			return new Vector4i((int)value.R, (int)value.G, (int)value.B, (int)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4i value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4i value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4us.</param>
		/// <returns>A Vector4us that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4us(Colord value)
		{
			return new Vector4us((ushort)value.R, (ushort)value.G, (ushort)value.B, (ushort)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4us value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colord(Vector4us value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4s.</param>
		/// <returns>A Vector4s that has all components equal to value.</returns>
		public static explicit operator Vector4s(Colord value)
		{
			return new Vector4s((short)value.R, (short)value.G, (short)value.B, (short)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4s value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4s value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4b.</param>
		/// <returns>A Vector4b that has all components equal to value.</returns>
		public static explicit operator Vector4b(Colord value)
		{
			return new Vector4b((byte)value.R, (byte)value.G, (byte)value.B, (byte)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4b value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		public static implicit operator Colord(Vector4b value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector4sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector4sb.</param>
		/// <returns>A Vector4sb that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector4sb(Colord value)
		{
			return new Vector4sb((sbyte)value.R, (sbyte)value.G, (sbyte)value.B, (sbyte)value.A);
		}
		/// <summary>
		/// Defines an implicit conversion of a Vector4sb value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A Colord that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static implicit operator Colord(Vector4sb value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3d.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3d.</param>
		/// <returns>A Vector3d that has all components equal to value.</returns>
		public static explicit operator Vector3d(Colord value)
		{
			return new Vector3d((double)value.R, (double)value.G, (double)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3d value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3d value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3f.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3f.</param>
		/// <returns>A Vector3f that has all components equal to value.</returns>
		public static explicit operator Vector3f(Colord value)
		{
			return new Vector3f((float)value.R, (float)value.G, (float)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3f value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3f value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3h.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3h.</param>
		/// <returns>A Vector3h that has all components equal to value.</returns>
		public static explicit operator Vector3h(Colord value)
		{
			return new Vector3h((Half)value.R, (Half)value.G, (Half)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3h value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3h value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3ul.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ul.</param>
		/// <returns>A Vector3ul that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3ul(Colord value)
		{
			return new Vector3ul((ulong)value.R, (ulong)value.G, (ulong)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ul value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colord(Vector3ul value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3l.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3l.</param>
		/// <returns>A Vector3l that has all components equal to value.</returns>
		public static explicit operator Vector3l(Colord value)
		{
			return new Vector3l((long)value.R, (long)value.G, (long)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3l value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3l value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3ui.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3ui.</param>
		/// <returns>A Vector3ui that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3ui(Colord value)
		{
			return new Vector3ui((uint)value.R, (uint)value.G, (uint)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3ui value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colord(Vector3ui value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3i.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3i.</param>
		/// <returns>A Vector3i that has all components equal to value.</returns>
		public static explicit operator Vector3i(Colord value)
		{
			return new Vector3i((int)value.R, (int)value.G, (int)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3i value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3i value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3us.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3us.</param>
		/// <returns>A Vector3us that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3us(Colord value)
		{
			return new Vector3us((ushort)value.R, (ushort)value.G, (ushort)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3us value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colord(Vector3us value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3s.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3s.</param>
		/// <returns>A Vector3s that has all components equal to value.</returns>
		public static explicit operator Vector3s(Colord value)
		{
			return new Vector3s((short)value.R, (short)value.G, (short)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3s value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3s value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3b.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3b.</param>
		/// <returns>A Vector3b that has all components equal to value.</returns>
		public static explicit operator Vector3b(Colord value)
		{
			return new Vector3b((byte)value.R, (byte)value.G, (byte)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3b value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		public static explicit operator Colord(Vector3b value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		/// <summary>
		/// Defines an explicit conversion of a Colord value to a Vector3sb.
		/// </summary>
		/// <param name="value">The value to convert to a Vector3sb.</param>
		/// <returns>A Vector3sb that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Vector3sb(Colord value)
		{
			return new Vector3sb((sbyte)value.R, (sbyte)value.G, (sbyte)value.B);
		}
		/// <summary>
		/// Defines an explicit conversion of a Vector3sb value to a Colord.
		/// </summary>
		/// <param name="value">The value to convert to a Colord.</param>
		/// <returns>A explicit that has all components equal to value.</returns>
		[CLSCompliant(false)]
		public static explicit operator Colord(Vector3sb value)
		{
			return new Colord((double)value.X, (double)value.Y, (double)value.Z, 1);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns the hash code for the current <see cref="Colord"/>.
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
		/// <returns>true if the obj parameter is a <see cref="Colord"/> object or a type capable
		/// of implicit conversion to a <see cref="Colord"/> object, and its value
		/// is equal to the current <see cref="Colord"/> object; otherwise, false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Colord) { return Equals((Colord)obj); }
			return false;
		}
		/// <summary>
		/// Returns a value that indicates whether the current instance and a specified
		/// vector have the same value.
		/// </summary>
		/// <param name="other">The vector to compare.</param>
		/// <returns>true if this vector and value have the same value; otherwise, false.</returns>
		public bool Equals(Colord other)
		{
			return this == other;
		}
		/// <summary>
		/// Returns a value that indicates whether two colors are equal.
		/// </summary>
		/// <param name="left">The first color to compare.</param>
		/// <param name="right">The second color to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool operator ==(Colord left, Colord right)
		{
			return left.R == right.R & left.G == right.G & left.B == right.B & left.A == right.A;
		}
		/// <summary>
		/// Returns a value that indicates whether two colors are not equal.
		/// </summary>
		/// <param name="left">The first color to compare.</param>
		/// <param name="right">The second color to compare.</param>
		/// <returns>true if the left and right are not equal; otherwise, false.</returns>
		public static bool operator !=(Colord left, Colord right)
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
		/// Writes the given <see cref="Colord"/> to an <see cref="Ibasa.IO.BinaryWriter">.
		/// </summary>
		public static void Write(this Ibasa.IO.BinaryWriter writer, Colord vector)
		{
			writer.Write(vector.R);
			writer.Write(vector.G);
			writer.Write(vector.B);
			writer.Write(vector.A);
		}
		/// <summary>
		/// Reads a <see cref="Colord"/> from an <see cref="Ibasa.IO.BinaryReader">.
		/// </summary>
		public static Colord ReadColord(this Ibasa.IO.BinaryReader reader)
		{
			return new Colord(reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble(), reader.ReadDouble());
		}
		#endregion
		#region Operations
		/// <summary>
		/// Adds two colors and returns the result.
		/// </summary>
		/// <param name="left">The first value to add.</param>
		/// <param name="right">The second value to add.</param>
		/// <returns>The sum of left and right.</returns>
		public static Colord Add(Colord left, Colord right)
		{
			return new Colord(left.R + right.R, left.G + right.G, left.B + right.B, left.A + right.A);
		}
		/// <summary>
		/// Subtracts one colors from another and returns the result.
		/// </summary>
		/// <param name="left">The value to subtract from (the minuend).</param>
		/// <param name="right">The value to subtract (the subtrahend).</param>
		/// <returns>The result of subtracting right from left (the difference).</returns>
		public static Colord Subtract(Colord left, Colord right)
		{
			return new Colord(left.R - right.R, left.G - right.G, left.B - right.B, left.A - right.A);
		}
		/// <summary>
		/// Returns the product of a color and scalar.
		/// </summary>
		/// <param name="color">The color to multiply.</param>
		/// <param name="scalar">The scalar to multiply.</param>
		/// <returns>The product of the left and right parameters.</returns>
		public static Colord Multiply(Colord color, double scalar)
		{
			return new Colord(color.R * scalar, color.G * scalar, color.B * scalar, color.A * scalar);
		}
		/// <summary>
		/// Divides a color by a scalar and returns the result.
		/// </summary>
		/// <param name="color">The color to be divided (the dividend).</param>
		/// <param name="scalar">The scalar to divide by (the divisor).</param>
		/// <returns>The result of dividing left by right (the quotient).</returns>
		public static Colord Divide(Colord color, double scalar)
		{
			return new Colord(color.R / scalar, color.G / scalar, color.B / scalar, color.A / scalar);
		}
		#endregion
		#region Equatable
		/// <summary>
		/// Returns a value that indicates whether two colors are equal.
		/// </summary>
		/// <param name="left">The first color to compare.</param>
		/// <param name="right">The second color to compare.</param>
		/// <returns>true if the left and right are equal; otherwise, false.</returns>
		public static bool Equals(Colord left, Colord right)
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
		public static bool All(Colord value)
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
		public static bool All(Colord value, Predicate<double> predicate)
		{
			return predicate(value.R) && predicate(value.G) && predicate(value.B) && predicate(value.A);
		}
		/// <summary>
		/// Determines whether any component of a color is non-zero.
		/// </summary>
		/// <param name="value">A color.</param>
		/// <returns>true if any components are non-zero; false otherwise.</returns>
		public static bool Any(Colord value)
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
		public static bool Any(Colord value, Predicate<double> predicate)
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
		public static Colord Negative(Colord color)
		{
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().R && Contract.Result<Colord>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().G && Contract.Result<Colord>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().B && Contract.Result<Colord>().B <= 1, "Result must be normalized.");
			return new Colord(1 - color.R, 1 - color.G, 1 - color.B, color.A);
		}
		/// <summary>
		/// Multiplies the RGB values of the color by the alpha value.
		/// </summary>
		/// <param name="color">The color to premultiply.</param>
		/// <returns>The premultipled color.</returns>
		public static Colord Premultiply(Colord color)
		{
			return new Colord(color.R * color.A, color.G * color.A, color.B * color.A, color.A);
		}
		/// <summary>
		/// Normalizes a color so all its RGB values are in the range [0.0, 1.0].
		/// </summary>
		/// <param name="color">The color to normalize.</param>
		/// <returns>The normalized color.</returns>
		public static Colord Normalize(Colord color)
		{
			Contract.Ensures(0 <= Contract.Result<Colord>().R && Contract.Result<Colord>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().G && Contract.Result<Colord>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().B && Contract.Result<Colord>().B <= 1, "Result must be normalized.");
			var bias = Functions.Min(Functions.Min(Functions.Min(color.R, color.G), color.B), 0);
			color -= new Colord(bias, bias, bias, 0);
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
		public static Colord Greyscale(Colord color)
		{
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().R && Contract.Result<Colord>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().G && Contract.Result<Colord>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().B && Contract.Result<Colord>().B <= 1, "Result must be normalized.");
			var greyscale = 0.2125 * color.R + 0.7154 * color.G + 0.0721 * color.B;
			return new Colord(greyscale, greyscale, greyscale, color.A);
		}
		/// <summary>
		/// Converts a color to black or white.
		/// </summary>
		/// <param name="color">The color to convert.</param>
		/// <returns>color in black or white.</returns>
		public static Colord BlackWhite(Colord color)
		{
			Contract.Requires(0 <= color.R && color.R <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.G && color.G <= 1, "color must be normalized.");
			Contract.Requires(0 <= color.B && color.B <= 1, "color must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().R && Contract.Result<Colord>().R <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().G && Contract.Result<Colord>().G <= 1, "Result must be normalized.");
			Contract.Ensures(0 <= Contract.Result<Colord>().B && Contract.Result<Colord>().B <= 1, "Result must be normalized.");
			var bw = Functions.Round(0.2125 * color.R + 0.7154 * color.G + 0.0721 * color.B);
			return new Colord(bw, bw, bw, color.A);
		}
		/// <summary>
		/// Gamma correct a color.
		/// </summary>
		/// <param name="color">Color to gamma correct.</param>
		/// <param name="gamma">Gamma value to use.</param>
		/// <returns>The gamma corrected color.</returns>
		public static Colord Gamma(Colord color, double gamma)
		{
			var r = Functions.Pow(color.R, gamma);
			var g = Functions.Pow(color.G, gamma);
			var b = Functions.Pow(color.B, gamma);
			var a = Functions.Pow(color.A, gamma);
			return new Colord(r, g, b, a);
		}
		#endregion
		#region Quantization
		public static Vector4l Quantize(int redBits, int greenBits, int blueBits, int alphaBits, Colord color)
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
		public static Vector3l Quantize(int redBits, int greenBits, int blueBits, Colord color)
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
		public static Colord Unquantized(int redBits, int greenBits, int blueBits, int alphaBits, Vector4l color)
		{
			Contract.Requires(0 <= redBits && redBits <= 63, "redBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= greenBits && greenBits <= 63, "greenBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= blueBits && blueBits <= 63, "blueBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= alphaBits && alphaBits <= 63, "alphaBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= color.X, "color must be positive.");
			Contract.Requires(0 <= color.Y, "color must be positive.");
			Contract.Requires(0 <= color.Z, "color must be positive.");
			Contract.Requires(0 <= color.W, "color must be positive.");
			Contract.Requires(0 <= Contract.Result<Colord>().R && Contract.Result<Colord>().R <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colord>().G && Contract.Result<Colord>().G <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colord>().B && Contract.Result<Colord>().B <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colord>().A && Contract.Result<Colord>().A <= 1, "result must be normalized.");
			var r = (double)color.X / ((1L << redBits) - 1);
			var g = (double)color.Y / ((1L << greenBits) - 1);
			var b = (double)color.Z / ((1L << blueBits) - 1);
			var a = (double)color.W / ((1L << alphaBits) - 1);
			return new Colord(r, g, b, a);
		}
		public static Colord Unquantized(int redBits, int greenBits, int blueBits, Vector3l color)
		{
			Contract.Requires(0 <= redBits && redBits <= 63, "redBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= greenBits && greenBits <= 63, "greenBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= blueBits && blueBits <= 63, "blueBits must be between 0 and 63 inclusive.");
			Contract.Requires(0 <= color.X, "color must be positive.");
			Contract.Requires(0 <= color.Y, "color must be positive.");
			Contract.Requires(0 <= color.Z, "color must be positive.");
			Contract.Requires(0 <= Contract.Result<Colord>().R && Contract.Result<Colord>().R <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colord>().G && Contract.Result<Colord>().G <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colord>().B && Contract.Result<Colord>().B <= 1, "result must be normalized.");
			Contract.Requires(0 <= Contract.Result<Colord>().A && Contract.Result<Colord>().A <= 1, "result must be normalized.");
			var r = (double)color.X / ((1L << redBits) - 1);
			var g = (double)color.Y / ((1L << greenBits) - 1);
			var b = (double)color.Z / ((1L << blueBits) - 1);
			return new Colord(r, g, b, 1);
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
		public static Colord Transform(Colord value, Func<double, double> transformer)
		{
			return new Colord(transformer(value.R), transformer(value.G), transformer(value.B), transformer(value.A));
		}
		/// <summary>
		/// Transforms the components of a color and returns the result.
		/// </summary>
		/// <param name="value">The color to transform.</param>
		/// <param name="transformer">A transform function to apply to each component.</param>
		/// <returns>The result of transforming each component of value.</returns>
		public static Colorf Transform(Colord value, Func<double, float> transformer)
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
		public static Colord Modulate(Colord left, Colord right)
		{
			return new Colord(left.R * right.R, left.G * right.G, left.B * right.B, left.A * right.A);
		}
		/// <summary>
		/// Returns the absolute value (per component).
		/// </summary>
		/// <param name="value">A color.</param>
		/// <returns>The absolute value (per component) of value.</returns>
		public static Colord Abs(Colord value)
		{
			return new Colord(Functions.Abs(value.R), Functions.Abs(value.G), Functions.Abs(value.B), Functions.Abs(value.A));
		}
		/// <summary>
		/// Returns a color that contains the lowest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first color.</param>
		/// <param name="value2">The second color.</param>
		/// <returns>The lowest of each component in left and the matching component in right.</returns>
		public static Colord Min(Colord value1, Colord value2)
		{
			return new Colord(Functions.Min(value1.R, value2.R), Functions.Min(value1.G, value2.G), Functions.Min(value1.B, value2.B), Functions.Min(value1.A, value2.A));
		}
		/// <summary>
		/// Returns a color that contains the highest value from each pair of components.
		/// </summary>
		/// <param name="value1">The first color.</param>
		/// <param name="value2">The second color.</param>
		/// <returns>The highest of each component in left and the matching component in right.</returns>
		public static Colord Max(Colord value1, Colord value2)
		{
			return new Colord(Functions.Max(value1.R, value2.R), Functions.Max(value1.G, value2.G), Functions.Max(value1.B, value2.B), Functions.Max(value1.A, value2.A));
		}
		/// <summary>
		/// Constrains each component to a given range.
		/// </summary>
		/// <param name="value">A color to constrain.</param>
		/// <param name="min">The minimum values for each component.</param>
		/// <param name="max">The maximum values for each component.</param>
		/// <returns>A color with each component constrained to the given range.</returns>
		public static Colord Clamp(Colord value, Colord min, Colord max)
		{
			return new Colord(Functions.Clamp(value.R, min.R, max.R), Functions.Clamp(value.G, min.G, max.G), Functions.Clamp(value.B, min.B, max.B), Functions.Clamp(value.A, min.A, max.A));
		}
		/// <summary>
		/// Constrains each component to the range 0 to 1.
		/// </summary>
		/// <param name="value">A color to saturate.</param>
		/// <returns>A color with each component constrained to the range 0 to 1.</returns>
		public static Colord Saturate(Colord value)
		{
			return new Colord(Functions.Saturate(value.R), Functions.Saturate(value.G), Functions.Saturate(value.B), Functions.Saturate(value.A));
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
		public static Colord Lerp(Colord color1, Colord color2, double amount)
		{
			return (1 - amount) * color1 + amount * color2;
		}
		#endregion
	}
}
