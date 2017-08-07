using HDCore;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HDCensorship.UI
{
    public partial class TimeLine : UserControl
    {
        private TimeCode m_RulerSpacingUnit = TimeCode.tcSec;
        private int m_RulerUnit = 100;

        private TimeCode m_RulerSpacing;
        private int m_Zoom;
        private int m_LengthRulerText;

        private Font m_RulerFont;

        private DateTime m_TimeStart;
        private DateTime m_TimeRecord;
        private DateTime m_TimePreview;
        private DateTime m_TimePlay;

        private TimeCode m_TimeCodeMouse;
        private DateTime m_TimeMouse;

        private DateTime m_TimeRulerBegin;
        private DateTime m_TimeRulerEnd;

        private DateTime m_TimeMarkIn;
        private DateTime m_TimeMarkOut;

        public TimeLine()
        {
            InitializeComponent();
            m_Zoom = 10;
            m_LengthRulerText = 8;
            m_RulerSpacing = new TimeCode(0, 0, 10, 0);
            m_RulerFont = new System.Drawing.Font("Arial", 11.0f);
            m_TimeStart = m_TimeRecord = m_TimePreview = m_TimePlay = m_TimeRulerBegin = DateTime.Now;
            m_TimeMarkIn = m_TimeMarkOut = DateTime.Now;
        }

        public int Zoom
        {
            get { return m_Zoom; }
            set
            {
                m_Zoom = value;
                TimeCode tc = new TimeCode();
                tc.FromFrame(m_RulerSpacingUnit.ToLong() * m_Zoom);
                if (tc.second != 0)
                    m_LengthRulerText = 8;
                else if (tc.minute != 0)
                    m_LengthRulerText = 5;
                else m_LengthRulerText = 2;
                m_RulerSpacing = tc;
                UpdateRuler();
                UpdateClip();
            }
        }

        public void UpdateRuler()
        {
            TimeCode startTimeCode = (TimeCode)m_TimeStart.TimeOfDay;

            TimeCode endTimeCode = (TimeCode)m_TimeRecord.TimeOfDay;
            if (endTimeCode < startTimeCode)
                endTimeCode += TimeCode.tcDay;

            Graphics gp = this.CreateGraphics();
            int widthTotal = (int)((endTimeCode - startTimeCode).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong()) +
                (int)Math.Ceiling(gp.MeasureString("00:00:00", m_RulerFont).Width);

            if (widthTotal <= this.Width)
            {
                hScroll.Visible = false;
                hScroll.Value = 0;
                pnClip.Height = this.Height - pnRuler.Height;
                pnRecord.Height = pnPreview.Height = pnPlay.Height = pnClip.Height + 10;
            }
            else
            {
                pnClip.Height = this.Height - pnRuler.Height - hScroll.Height;
                pnRecord.Height = pnPreview.Height = pnPlay.Height = pnClip.Height + 10;
                hScroll.Visible = true;
            }

            if (widthTotal < this.Width)
            {
                widthTotal = this.Width;

                endTimeCode.FromFrame(widthTotal * m_RulerSpacing.ToLong() / m_RulerUnit);
                endTimeCode += startTimeCode;
            }

            if (hScroll.Visible)
            {
                TimeCode tcAdd = new TimeCode();
                tcAdd.FromFrame((int)((hScroll.Value * widthTotal / hScroll.Maximum) * m_RulerSpacing.ToLong() / m_RulerUnit));
                startTimeCode += tcAdd;
                m_TimeRulerBegin = m_TimeStart.AddMilliseconds(tcAdd.ToLong() * 40);
            }
            else
            {
                m_TimeRulerBegin = m_TimeStart;
            }
            m_TimeRulerEnd = m_TimeRulerBegin.AddMilliseconds(this.Width * m_RulerSpacing.ToLong() * 40 / m_RulerUnit);

            Bitmap bmp = new Bitmap(pnRuler.Width, pnRuler.Height);
            gp = Graphics.FromImage(bmp);
            gp.Clear(pnRuler.BackColor);

            if (m_TimeMarkIn <= m_TimeMarkOut)
            {
                if (((m_TimeRulerBegin <= m_TimeMarkIn && m_TimeMarkIn <= m_TimeRulerEnd) ||
                    ((m_TimeRulerBegin <= m_TimeMarkOut && m_TimeMarkOut <= m_TimeRulerEnd))) ||
                    (m_TimeMarkIn <= m_TimeRulerBegin && m_TimeRulerEnd <= m_TimeMarkOut))
                {
                    int xB = 0, xE = 0;
                    if (m_TimeMarkIn <= m_TimeRulerBegin)
                        xB = 0;
                    else
                        xB = (int)(((TimeCode)(m_TimeMarkIn - m_TimeRulerBegin)).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong());
                    if (m_TimeRulerEnd <= m_TimeRulerBegin)
                        xE = 0;
                    else
                        xE = (int)(((TimeCode)(m_TimeMarkOut - m_TimeRulerBegin)).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong());
                    gp.FillRectangle(Brushes.Yellow, xB, 0, xE - xB, pnRuler.Height);
                }
            }
            else
            {
                if (m_TimeRulerBegin <= m_TimeMarkIn && m_TimeMarkIn <= m_TimeRulerEnd)
                {
                    int xB = 0;
                    if (m_TimeMarkIn <= m_TimeRulerBegin)
                        xB = 0;
                    else
                        xB = (int)(((TimeCode)(m_TimeMarkIn - m_TimeRulerBegin)).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong());
                    gp.FillRectangle(Brushes.Yellow, xB, 0, 1, pnRuler.Height);
                }
            }

            int maxWidth = this.Width - (int)Math.Ceiling(gp.MeasureString("00:00:00", m_RulerFont).Width);

            TimeCode tcCur = startTimeCode.Celling();
            while (tcCur.ToLong() % m_RulerSpacing.ToLong() != 0)
                tcCur += TimeCode.tcSec;

            Pen pen = new Pen(Brushes.Black);
            // Vẽ dòng kẻ dưới
            gp.DrawLine(pen, 0, bmp.Height - 1, bmp.Width, bmp.Height - 1);

            // Vẽ vạch nhỏ trước vạch đầu tiên
            tcCur -= m_RulerSpacing;
            for (int i = (int)((tcCur.ToLong() - startTimeCode.ToLong()) * m_RulerUnit /
                m_RulerSpacing.ToLong()); i <= this.Width; i += m_RulerUnit)
            {
                // Vẽ vạch
                int xB = i, yB = pnRuler.Height;
                gp.DrawLine(pen, xB, yB, xB, yB - 10);
                // Vẽ time code
                if (tcCur.hour > 23)
                    tcCur.hour -= 24;
                string tc = tcCur.ToHDString();
                tc = tc.Substring(0, m_LengthRulerText);
                gp.DrawString(tc, m_RulerFont, Brushes.Black, xB - 7, yB - 12 - gp.MeasureString(tc, m_RulerFont).Height);
                tcCur += m_RulerSpacing;

                // Vẽ vạch nhỏ
                for (int j = 1; j < 10; j++)
                {
                    int xN = i + j * m_RulerUnit / 10, yN = pnRuler.Height;
                    gp.DrawLine(pen, xN, yN, xN, yN - 5);
                }
            }
            gp.Dispose();
            pnRuler.BackgroundImage = bmp;
        }

        private void UpdateClip()
        {
            if (m_TimePlay >= m_TimeRulerBegin && m_TimePlay <= m_TimeRulerEnd)
            {
                int xPlay = (int)(((TimeCode)(m_TimePlay - m_TimeRulerBegin)).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong());
                pnPlay.Location = new Point(xPlay, pnPlay.Location.Y);
                pnPlay.Visible = true;
            }
            else
                pnPlay.Visible = false;

            if (m_TimePreview >= m_TimeRulerBegin && m_TimePreview <= m_TimeRulerEnd)
            {
                int xPreview = (int)(((TimeCode)(m_TimePreview - m_TimeRulerBegin)).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong());
                pnPreview.Location = new Point(xPreview, pnPreview.Location.Y);
                pnPreview.Visible = true;
            }
            else
                pnPreview.Visible = false;

            if (m_TimeRecord >= m_TimeRulerBegin && m_TimeRecord <= m_TimeRulerEnd)
            {
                int xRecord = (int)(((TimeCode)(m_TimeRecord - m_TimeRulerBegin)).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong());
                pnRecord.Location = new Point(xRecord, pnRecord.Location.Y);
                pnRecord.Visible = true;
                pnClip.Width = xRecord;
                pnClip.Visible = true;
            }
            else
            {
                pnRecord.Visible = false;
                if (m_TimeRecord < m_TimeRulerBegin)
                    pnClip.Visible = false;
                else
                {
                    pnClip.Width = this.Width;
                    pnClip.Visible = true;
                }
            }
        }

        public TimeCode TimeCodeMouse
        {
            get { return m_TimeCodeMouse; }
        }

        public DateTime TimeMouse
        {
            get { return m_TimeMouse; }
        }

        public DateTime TimeStart
        {
            get { return m_TimeStart; }
            set
            {
                if (value != m_TimeStart)
                {
                    m_TimeStart = value;
                    if (m_TimePreview < m_TimeStart)
                        m_TimePreview = m_TimeStart;
                    if (m_TimeRecord < m_TimeStart)
                        m_TimeRecord = m_TimeStart;
                    if (m_TimePlay < m_TimeStart)
                        m_TimePlay = m_TimeStart;
                    UpdateRuler();
                    UpdateClip();
                }
            }
        }

        public DateTime TimeRecord
        {
            get { return m_TimeRecord; }
            set
            {
                if (value != m_TimeRecord && value >= m_TimeStart)
                {
                    m_TimeRecord = value;
                    if (m_TimePreview > m_TimeRecord)
                        m_TimePreview = m_TimeRecord;
                    if (m_TimePlay > m_TimeRecord)
                        m_TimePlay = m_TimeRecord;
                    UpdateClip();
                }
            }
        }

        public DateTime TimePreview
        {
            get { return m_TimePreview; }
            set
            {
                if (value != m_TimePreview && value <= m_TimeRecord && value >= m_TimeStart)
                {
                    m_TimePreview = value;
                    UpdateClip();
                }
            }
        }

        public DateTime TimePlay
        {
            get { return m_TimePlay; }
            set
            {
                if (value != m_TimePlay && value <= m_TimeRecord && value >= m_TimeStart)
                {
                    m_TimePlay = value;
                    UpdateClip();
                }
            }
        }

        public DateTime TimeMarkIn
        {
            get { return m_TimeMarkIn; }
            set
            {
                if (value != m_TimeMarkIn)
                {
                    m_TimeMarkIn = value;
                    UpdateRuler();
                }
            }
        }

        public DateTime TimeMarkOut
        {
            get { return m_TimeMarkOut; }
            set
            {
                if (value != m_TimeMarkOut)
                {
                    m_TimeMarkOut = value;
                    UpdateRuler();
                }
            }
        }

        public void SetTimeRecordPlay(DateTime record, DateTime play)
        {
            bool change = false;

            if (record != m_TimeRecord && record >= m_TimeStart)
            {
                change = true;
                m_TimeRecord = record;
            }

            if (play != m_TimePlay && play <= m_TimeRecord && play >= m_TimeStart)
            {
                change = true;
                m_TimePlay = play;
            }

            if (m_TimePlay > m_TimeRecord)
                m_TimePlay = m_TimeRecord;

            if (change)
                UpdateClip();
        }

        private void hScroll_Scroll(object sender, ScrollEventArgs e)
        {
            UpdateRuler();
            UpdateClip();
        }

        private void TimeLine2_SizeChanged(object sender, EventArgs e)
        {
            UpdateRuler();
            UpdateClip();
        }

        private void pnRuler_MouseMove(object sender, MouseEventArgs e)
        {
            m_TimeMouse = m_TimeRulerBegin.AddMilliseconds(e.X * m_RulerSpacing.ToLong() * 40 / m_RulerUnit);
            m_TimeCodeMouse = (TimeCode)m_TimeMouse.TimeOfDay;
            //TimeCode tc = new TimeCode();
            //tc.FromLong(e.X * m_RulerSpacing.ToLong() / m_RulerUnit);
            //TimeCode tcStart = (TimeCode)m_TimeRulerBegin.TimeOfDay;
            //tcStart += tc;
            //if (tcStart.hour > 23)
            //    tcStart.hour -= 24;
            //m_TimeCodeMouse = tcStart;
            this.OnMouseMove(e);
        }

        public void GoToPreview()
        {
            if (m_TimePreview <= m_TimeRulerBegin || m_TimePreview >= m_TimeRulerEnd)
            {
                TimeCode startTimeCode = (TimeCode)m_TimeStart.TimeOfDay;

                TimeCode endTimeCode = (TimeCode)m_TimeRecord.TimeOfDay;
                if (endTimeCode < startTimeCode)
                    endTimeCode += TimeCode.tcDay;

                Graphics gp = this.CreateGraphics();
                int widthTotal = (int)((endTimeCode - startTimeCode).ToLong() * m_RulerUnit / m_RulerSpacing.ToLong()) +
                    (int)Math.Ceiling(gp.MeasureString("00:00:00", m_RulerFont).Width);


                long frame = (long)((m_TimePreview - m_TimeStart).TotalMilliseconds / 40);
                try
                {
                    hScroll.Value = (int)(frame * m_RulerUnit * hScroll.Maximum / m_RulerSpacing.ToLong() / widthTotal);
                }
                catch { }

                UpdateRuler();
                UpdateClip();
            }
        }

        public DateTime tSeek;
        private void pnRuler_MouseUp(object sender, MouseEventArgs e)
        {
            TimeCode tc = new TimeCode();
            tc.FromFrame(e.X * m_RulerSpacing.ToLong() / m_RulerUnit);
            TimeCode tcStart = (TimeCode)m_TimeRulerBegin.TimeOfDay;
            tcStart += tc;
            tSeek = new DateTime(m_TimeRulerBegin.Year, m_TimeRulerBegin.Month, m_TimeRulerBegin.Day).AddMilliseconds(tcStart.ToLong() * 40);
            this.OnMouseUp(e);
        }
    }
}
