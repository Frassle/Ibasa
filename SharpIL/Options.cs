using Ibasa.Numerics;

namespace Ibasa.SharpIL
{	
	public enum Quality
	{
		Fastest,
		Low,
        Normal,
		High,
		Best,
	}

	public sealed class Options
	{
        public static readonly Vector3d MetricUniform = new Vector3d(1.0);
        public static readonly Vector3d MetricPerceptual = new Vector3d(0.2126, 0.7152, 0.0722);

        public static readonly Options Default = new Options(Quality.Normal, MetricPerceptual, true);

        public Options(Quality quality, Vector3d metric, bool weightColordByAlpha)
        {
            Quality = quality;
            Metric = Vector.Clamp(metric, Vector3d.Zero, Vector3d.One);
            WeightColordByAlpha = weightColordByAlpha;
        }

		public Quality Quality {get; private set;}
        public Vector3d Metric { get; private set; }
        public bool WeightColordByAlpha { get; private set; }
	}
}