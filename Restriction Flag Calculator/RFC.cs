using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restriction_Flag_Calculator
{
    public partial class RFC : Form
    {
        public RFC()
        {
            InitializeComponent();
        }

        private void EditorChanged(object sender, EventArgs e)
        {
            NumUpDown_Result.ValueChanged -= ResultChanged;

            byte FirstByte = (byte)((Global.Checked ? 1 : 0) + (DinPNayruL.Checked ? 4 : 0) + (FaroreWind.Checked ? 16 : 0) + (SunSong.Checked ? 64 : 0));
            byte SecondByte = (byte)((WarpSongs.Checked ? 1 : 0) + (Ocarina.Checked ? 4 : 0) + (Hookshot.Checked ? 16 : 0) + (TradingItems.Checked ? 64 : 0));
            byte ThirdByte = (byte)((Bottles.Checked ? 1 : 0) + (AButton.Checked ? 4 : 0) + (BButton.Checked ? 16 : 0) + (Health.Checked ? 64 : 0));

            byte[] Result = new byte[4];

            Result[0] = FirstByte;
            Result[1] = SecondByte;
            Result[2] = ThirdByte;
            Result[3] = Convert.ToByte(NumUpDown_SceneNumber.Value);

            NumUpDown_Result.Text = BitConverter.ToUInt32(Result, 0).ToString();

            NumUpDown_Result.ValueChanged += ResultChanged;
        }

        private void ResultChanged(object sender, EventArgs e)
        {
            foreach (Control C in this.Controls)
                if (C is CheckBox)
                    (C as CheckBox).CheckedChanged -= EditorChanged;

            NumUpDown_SceneNumber.ValueChanged -= EditorChanged;

            uint Result = (uint)(NumUpDown_Result.Value);

            byte[] Bytes = BitConverter.GetBytes(Result);

            byte SceneID = Bytes[3];
            byte ThirdByte = Bytes[2];
            byte SecondByte = Bytes[1];
            byte FirstByte = Bytes[0];

            NumUpDown_SceneNumber.Value = Convert.ToInt32(SceneID);

            Global.Checked = Convert.ToBoolean(FirstByte & 1) | Convert.ToBoolean(FirstByte & 2);
            DinPNayruL.Checked = Convert.ToBoolean(FirstByte & 4) | Convert.ToBoolean(FirstByte & 8);
            FaroreWind.Checked = Convert.ToBoolean(FirstByte & 16) | Convert.ToBoolean(FirstByte & 32);
            SunSong.Checked = Convert.ToBoolean(FirstByte & 64) | Convert.ToBoolean(FirstByte & 128);

            WarpSongs.Checked = Convert.ToBoolean(SecondByte & 1) | Convert.ToBoolean(FirstByte & 2);
            Ocarina.Checked = Convert.ToBoolean(SecondByte & 4) | Convert.ToBoolean(FirstByte & 8);
            Hookshot.Checked = Convert.ToBoolean(SecondByte & 16) | Convert.ToBoolean(FirstByte & 32);
            TradingItems.Checked = Convert.ToBoolean(SecondByte & 64) | Convert.ToBoolean(FirstByte & 128);

            Bottles.Checked = Convert.ToBoolean(ThirdByte & 1) | Convert.ToBoolean(FirstByte & 2);
            AButton.Checked = Convert.ToBoolean(ThirdByte & 4) | Convert.ToBoolean(FirstByte & 8);
            BButton.Checked = Convert.ToBoolean(ThirdByte & 16) | Convert.ToBoolean(FirstByte & 32);
            Health.Checked = Convert.ToBoolean(ThirdByte & 64) | Convert.ToBoolean(FirstByte & 128);

            foreach (Control C in this.Controls)
                if (C is CheckBox)
                    (C as CheckBox).CheckedChanged += EditorChanged;

            NumUpDown_SceneNumber.ValueChanged += EditorChanged;
        }
    }
}
