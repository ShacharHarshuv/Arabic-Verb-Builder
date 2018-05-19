using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail; 

namespace ArabicVerbBuilder
{
    public partial class MainWin : Form
    {
        float fontSize;

        public MainWin()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            MoodIn.DataSource = Mood_wrapper.Options;
            //PronounIn.DataSource = Pronoun_wrapper.Options;
            setPronounsOptions();
            setCaseOptions();
            setCaseAndKnownVisibility();
            setVowelInOptions();
            setVowelVisibility();
            setActiveVisibility();
            fontSize = OutputBox.Font.Size;
        }
        private void setCaseOptions()
        {
            CaseIn.Items.Add("Nominative - المَرْفُوع");
            CaseIn.Items.Add("Accusative - المَنْصُوب");
            CaseIn.Items.Add("Genitive - المَجْرُور");
            CaseIn.SelectedIndex = 0; 
            
        }
        private void setVowelInOptions()
        {
            VowelIn.Items.Add("َ"); //fatha
            VowelIn.Items.Add("ِ"); //kasra
            VowelIn.Items.Add("ُ"); //dama
            VowelIn.Text = "ُ";
        }
        private void setCaseAndKnownVisibility()
        {
            Mood mood = (Mood)(Mood_wrapper)MoodIn.SelectedItem;
            switch (mood)
            {
                case Mood.Past:
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                case Mood.imperative:
                    CaseIn.Enabled = false;
                    caseLabel.Enabled = false;
                    knownCheckBox.Enabled = false;
                    break;
                default:
                    CaseIn.Enabled = true;
                    caseLabel.Enabled = true;
                    knownCheckBox.Enabled = true;
                    break;
            }
        }
        private void setVowelVisibility()
        {
            if ((int)FormIn.Value == 1)
            {
                VowelIn.Enabled = true;
                pastVowelKasra_CheckBox.Enabled = true;
            }
            else
            {
                VowelIn.Enabled = false;
                pastVowelKasra_CheckBox.Enabled = false;
            }
                
        }
        private void setActiveVisibility()
        {
            Mood mood = (Mood)(Mood_wrapper)MoodIn.SelectedItem;
            switch (mood)
            {
                case Mood.Past:
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    passiveCheckBox.Enabled = true;
                    break;
                default:
                    passiveCheckBox.Enabled = false;
                    break;
            }
        }
       
        private void setPronounsOptions()
        {
            object current_pronoun = PronounIn.SelectedItem;

            switch((Mood)(Mood_wrapper)MoodIn.SelectedItem) //what does selected item actually return??
            {
                case Mood.imperative:
                    PronounIn.DataSource = Pronoun_wrapper.Options_imperative;
                    break;
                case Mood.agentActive:
                case Mood.agentPassive:
                    PronounIn.DataSource = Pronouns_Partciple_wrapper.Options;
                    break;
                case Mood.infinitive:
                    PronounIn.DataSource = Pronouns_Partciple_wrapper.Options_masdar;
                    break;
                default:
                    PronounIn.DataSource = Pronoun_wrapper.Options;
                    break;
            }
            PronounIn.SelectedItem = getMatchingPronoun(current_pronoun, (Array)PronounIn.DataSource);
        }

        private object getMatchingPronoun(object pronoun, Array options)
        {
            if (pronoun == null) return options.GetValue(0);
            foreach (object option in options)
            {
                if (pronoun.ToString() == option.ToString())
                    return option;
            }

            foreach (object option in options)
            {
                if (((IEnumWrapper)pronoun).IsLike((IEnumWrapper)option))
                    return option;
            }

            return null;
        }

        private void BuildButton_Click(object sender, EventArgs e)
        {
            try
            {
                //get form, root, and mood
                int form = (int)FormIn.Value;
                Root root = new Root(RootIn.Text);
                Mood_wrapper mood = (Mood_wrapper)MoodIn.SelectedItem;
                
                //get pronoun - 2 options! - verb, noun
                Pronoun_wrapper pronoun = null;
                Pronouns_Partciple_wrapper pronoun_participle = null;
                switch((Mood)mood)
                {
                    case Mood.agentActive:
                    case Mood.agentPassive:
                    case Mood.infinitive:
                        pronoun_participle = (Pronouns_Partciple_wrapper)PronounIn.SelectedItem;
                        break;
                    default:
                        pronoun = (Pronoun_wrapper)PronounIn.SelectedItem;
                        break;
                }
                
                //get vowel
                char vowel = VowelIn.Text[0];
                bool pastVowelKasra = pastVowelKasra_CheckBox.Checked;
                //get passive
                bool passive = passiveCheckBox.Checked; 
                //noun case and known option
                int ncase = CaseIn.SelectedIndex + 1; // the case can be 1, 2, or 3
                bool known = knownCheckBox.Checked; 

                //send to verb generator
                Verb_Container verb = new Verb_Container(form, root, mood, pronoun, pronoun_participle, passive, vowel, pastVowelKasra, ncase, known);

                //print the output
                OutputBox.Text = verb.ToString();
                OutputBox.Font = new Font(OutputBox.Font.FontFamily, fontSize);
                using (Graphics g = OutputBox.CreateGraphics())
                {
                    SizeF sf = g.MeasureString(OutputBox.Text, OutputBox.Font, 1000);
                    float width = sf.Width;
                    if (width > OutputBox.Width)
                    {
                        OutputBox.Font = new Font(OutputBox.Font.FontFamily, OutputBox.Font.Size - 10);
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void MoodIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPronounsOptions();
            setActiveVisibility();
            setCaseAndKnownVisibility(); 

        }

        private void FormIn_ValueChanged(object sender, EventArgs e)
        {
            setVowelVisibility();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void PronounIn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void MainWin_Load(object sender, EventArgs e)
        {

        }

        private void RootIn_TextChanged(object sender, EventArgs e)
        {
            if (FormIn.Value == 1)
            {
                if ((RootIn.Text.Length >= 3) &&
                (RootIn.Text[2] == 'ي' || RootIn.Text[2] == 'و') )
                {
                    pastVowelKasra_CheckBox.Enabled = false;
                }
                else
                    pastVowelKasra_CheckBox.Enabled = true;
            }
            
        }

        private void reportButton_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Are you sure you want to report a mistake?", "Report Mistake", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    //using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
            //    //{
            //    //    //client.Host = "smtp.gmail.com";
            //    //    //client.Port = 25;
            //    //    client.Credentials = new System.Net.NetworkCredential("ranh.android@gmail.com", "024801706");
            //    //    //client.UseDefaultCredentials = false;
            //    //    client.EnableSsl = true;
            //    //    client.Send("ran.harshuv@gmail.com", "shachar.harshuv@gmail.com", "error report", "report data");
            //    //}


            //    ////MailMessage mail = new MailMessage("ran.harshuv@gmail.com", "shachar@gmail.com");
            //    ////SmtpClient client = new SmtpClient();
            //    ////client.Port = 25;
            //    ////client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    ////client.UseDefaultCredentials = false;
            //    ////client.Host = "smtp.gmail.com";
            //    ////mail.Subject = "this is a test email.";
            //    ////mail.Body = "this is my test email body";
            //    ////client.Send(mail);

            //}

            MessageBox.Show("Send input setting that lead to a wrong verb to: shachar.harshuv@gmail.com. Thanks for you help! ");

        }
    }
}
