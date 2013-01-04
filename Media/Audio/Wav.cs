using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ibasa.Media.Audio
{
    public sealed class Wav
    {
        public enum FormatTag
        {
            Unknown = 0x0000,    /*  microsoft corporation  */
            Pcm = 0x0001,
            Adpcm = 0x0002,    /*  microsoft corporation  */
            Ieee_float = 0x0003,    /*  microsoft corporation  */
            /*  ieee754: range (+1, -1]  */
            /*  32-bit/64-bit format as defined by */
            /*  msvc++ float/double type */
            Ibm_cvsd = 0x0005,    /*  ibm corporation  */
            Alaw = 0x0006,    /*  microsoft corporation  */
            Mulaw = 0x0007,    /*  microsoft corporation  */
            Oki_adpcm = 0x0010,    /*  oki  */
            Dvi_adpcm = 0x0011,    /*  intel corporation  */
            Ima_adpcm = Dvi_adpcm, /*  intel corporation  */
            Mediaspace_adpcm = 0x0012,    /*  videologic  */
            Sierra_adpcm = 0x0013,    /*  sierra semiconductor corp  */
            G723_adpcm = 0x0014,    /*  antex electronics corporation  */
            Digistd = 0x0015,    /*  dsp solutions, inc.  */
            Digifix = 0x0016,    /*  dsp solutions, inc.  */
            Dialogic_oki_adpcm = 0x0017,    /*  dialogic corporation  */
            Mediavision_adpcm = 0x0018,    /*  media vision, inc. */
            Yamaha_adpcm = 0x0020,    /*  yamaha corporation of america  */
            Sonarc = 0x0021,    /*  speech compression  */
            Dspgroup_truespeech = 0x0022,    /*  dsp group, inc  */
            Echosc1 = 0x0023,    /*  echo speech corporation  */
            Audiofile_af36 = 0x0024,    /*    */
            Aptx = 0x0025,    /*  audio processing technology  */
            Audiofile_af10 = 0x0026,    /*    */
            Dolby_ac2 = 0x0030,    /*  dolby laboratories  */
            Gsm610 = 0x0031,    /*  microsoft corporation  */
            Msnaudio = 0x0032,    /*  microsoft corporation  */
            Antex_adpcme = 0x0033,    /*  antex electronics corporation  */
            Control_res_vqlpc = 0x0034,    /*  control resources limited  */
            Digireal = 0x0035,    /*  dsp solutions, inc.  */
            Digiadpcm = 0x0036,    /*  dsp solutions, inc.  */
            Control_res_cr10 = 0x0037,    /*  control resources limited  */
            Nms_vbxadpcm = 0x0038,    /*  natural microsystems  */
            Cs_imaadpcm = 0x0039,    /* crystal semiconductor ima adpcm */
            Echosc3 = 0x003a,    /* echo speech corporation */
            Rockwell_adpcm = 0x003b,    /* rockwell international */
            Rockwell_digitalk = 0x003c,    /* rockwell international */
            Xebec = 0x003d,    /* xebec multimedia solutions limited */
            G721_adpcm = 0x0040,    /*  antex electronics corporation  */
            G728_celp = 0x0041,    /*  antex electronics corporation  */
            Mpeg = 0x0050,    /*  microsoft corporation  */
            Mpeglayer3 = 0x0055,    /*  iso/mpeg layer3 format tag */
            Cirrus = 0x0060,    /*  cirrus logic  */
            Espcm = 0x0061,    /*  ess technology  */
            Voxware = 0x0062,    /*  voxware inc  */
            Canopus_atrac = 0x0063,    /*  canopus, co., ltd.  */
            G726_adpcm = 0x0064,    /*  apicom  */
            G722_adpcm = 0x0065,    /*  apicom      */
            Dsat = 0x0066,    /*  microsoft corporation  */
            Dsat_display = 0x0067,    /*  microsoft corporation  */
            Softsound = 0x0080,    /*  softsound, ltd.      */
            Rhetorex_adpcm = 0x0100,    /*  rhetorex inc  */
            Creative_adpcm = 0x0200,    /*  creative labs, inc  */
            Creative_fastspeech8 = 0x0202,    /*  creative labs, inc  */
            Creative_fastspeech10 = 0x0203,    /*  creative labs, inc  */
            Quarterdeck = 0x0220,    /*  quarterdeck corporation  */
            Fm_towns_snd = 0x0300,    /*  fujitsu corp.  */
            Btv_digital = 0x0400,    /*  brooktree corporation  */
            Oligsm = 0x1000,    /*  ing c. olivetti & c., s.p.a.  */
            Oliadpcm = 0x1001,    /*  ing c. olivetti & c., s.p.a.  */
            Olicelp = 0x1002,    /*  ing c. olivetti & c., s.p.a.  */
            Olisbc = 0x1003,    /*  ing c. olivetti & c., s.p.a.  */
            Oliopr = 0x1004,    /*  ing c. olivetti & c., s.p.a.  */
            Lh_codec = 0x1100,    /*  lernout & hauspie  */
            Norris = 0x1400,    /*  Norris Communications, Inc.  */
        };

        public Wav(System.IO.Stream stream)
        {
            Riff.RiffReader reader = new Riff.RiffReader(stream);

            if (!reader.Read() || !reader.Type.HasValue || reader.Type.Value != "WAVE")
            {
                throw new System.IO.InvalidDataException("Not a WAVE stream.");
            }

            while (reader.Read())
            {
                if (reader.Id == "fmt ")
                {
                    ParseFmt(reader);
                }
                if (reader.Id == "data")
                {
                    ParseData(reader);
                }
            }
        }

        public Ibasa.SharpAL.Format Format { get; private set; }
        public byte[] Data { get; private set; }

        public int Frequency { get; private set; }

        private void ParseFmt(Riff.RiffReader reader)
        {
            Ibasa.IO.BinaryReader binreader = new IO.BinaryReader(reader.Data);

            FormatTag tag;
            int channels, bytesPerSecond, blockAlignment,bitsPerSample, size;
            if (reader.Length < 16)
            {
                throw new System.IO.InvalidDataException("Invalid fmt chunk.");
            }
            else
            {
                tag = (FormatTag)binreader.ReadInt16();
                channels = binreader.ReadInt16();
                Frequency = binreader.ReadInt32();
                bytesPerSecond = binreader.ReadInt32();
                blockAlignment = binreader.ReadInt16();
                bitsPerSample = binreader.ReadInt16();
            }

            if (reader.Length >= 18)
            {
                size = binreader.ReadInt16();
            }

            if (tag == FormatTag.Pcm)
            {
                if (bitsPerSample == 8)
                {
                    Format = new Ibasa.SharpAL.Formats.Pcm8(channels);
                }
                else if (bitsPerSample == 16)
                {
                    Format = new Ibasa.SharpAL.Formats.Pcm16(channels);
                }
                else
                {
                    throw new NotSupportedException(string.Format("PCM {0} bit data is not supported.", bitsPerSample));
                }
            }
            else
            {
                throw new NotSupportedException(string.Format("{0} is not supported.", tag));
            }
        }

        private void ParseData(Riff.RiffReader reader)
        {
            Data = new byte[reader.Length];
            reader.Data.Read(Data, 0, Data.Length);
        }
    }
}
