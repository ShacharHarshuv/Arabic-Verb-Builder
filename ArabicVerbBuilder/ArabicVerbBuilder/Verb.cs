using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicVerbBuilder
{
    public class Verb : ArWord
    {
        static Exception not_supported = new Exception("Feature not supported in this build.");
        static Exception invalid_input = new Exception("Invalid input");

        int form;
        Root root;
        int fi;
        int ai;
        int li; //indexes of the root in the verb.
        Mood mood;
        Pronoun pronoun;
        Pronouns_Partciple pronoun_principle;
        bool passive;
        char vowel; //AIN ALFIEL vowel in nonPast form 1
        bool pastVowelKasra;
        int ncase; //case for nouns - 1, 2, 3
        bool known; //is the noun "known" - "the ..."
        int option_index; //in case we have more than one option to build, we will know if we need to build option #1 or #2
        bool isThereAnotherOption = false;
        bool isDual = false;

        public bool IsThereAnotherOption { get { return isThereAnotherOption; } }

        public Verb(int _form, Root _root, Mood _mood, Pronoun _pronoun, Pronouns_Partciple _pronoun_principle, bool _Passive, char _vowel, bool _pastVowelKasra, int _ncase, bool _known, int _option_index = 0) 
        {
            form = _form;
            root = _root;
            mood = _mood;
            pronoun = _pronoun;
            pronoun_principle = _pronoun_principle;
            passive = _Passive;
            vowel = _vowel;
            pastVowelKasra = _pastVowelKasra;
            ncase = _ncase;
            known = _known;
            option_index = _option_index;
            buildVerb();
            
        }

        private void buildVerb()
        {
            //if the pronoun is dual - we will build according to the corresponsing non-dual required pronoun
            switch(pronoun)
            {
                case Pronoun.d_you:
                    if (mood == Mood.Past)
                        pronoun = Pronoun.mp_you;
                    else
                        pronoun = Pronoun.ms_you;
                    isDual = true;
                    break;
                case Pronoun.md_they:
                    pronoun = Pronoun.he;
                    isDual = true;
                    break;
                case Pronoun.fd_they:
                    pronoun = Pronoun.she;
                    isDual = true;
                    break;
            }
            //in LWLY form 1 we need to determine lam alfiel
            if (form == 1 && (root.L == 'ي' || root.L == 'و'))
            {
                switch(vowel)
                {
                    case Letter.fatha:
                        pastVowelKasra = true;
                        break;
                    case Letter.kasra:
                    case Letter.dama:
                        pastVowelKasra = false;
                        break;
                }
            }
            
            buildNormal(); //HASHLEMIM Active

            if (passive)
            {
                Passive();
            }

            if (root.F == 'ي' || root.F == 'و')
                FWFY();
            if (root.A == 'ي' || root.A == 'و')
                AWAY();
            if (root.L == 'ي' || root.L == 'و')
                LWLY();
            if (root.A == root.L || form == 9)
                Doubles();
            
            
            Hamza();

        }

        private void InitVerb(string input)
        {
            Init(input);
            for (int i = 0; i < Count; i++)
            {
                switch (this[i].Cons)
                {
                    case 'ف':
                        fi = i;
                        changeCons(i, root.F);
                        break;
                    case 'ع':
                        ai = i;
                        changeCons(i, root.A);
                        break;
                    case 'ل':
                        li = i;
                        changeCons(i, root.L);
                        break;
                }
            }
        }

        private void buildNormal()
        {
            switch(form)
            {
                case 1:
                    Form1();
                    break;
                case 2:
                    Form2();
                    break;
                case 3:
                    Form3();
                    break;
                case 4:
                    Form4();
                    break;
                case 5:
                    Form5();
                    break;
                case 6:
                    Form6();
                    break;
                case 7:
                    Form7();
                    break;
                case 8:
                    Form8();
                    break;
                case 9:
                    Form9();
                    break;
                case 10:
                    Form10();
                    break;
                default:
                    throw invalid_input;

            }
            setPostFix();
            if (isDual)
            {
                this[Count - 1].Vowel = '0'; //remove the last vowel before insertion of the dual postfix
                addPostfix(Postfix_dual);
            }

            

        }

        private char getEITN()
        {
            switch (pronoun)
            {
                case Pronoun.I:
                    return 'أ';
                case Pronoun.he:
                case Pronoun.m_they:
                case Pronoun.f_they:
                    return 'ي';
                case Pronoun.ms_you:
                case Pronoun.fs_you:
                case Pronoun.she:
                case Pronoun.mp_you:
                case Pronoun.fp_you:
                    return 'ت';
                case Pronoun.we:
                    return 'ن';
                default:
                    throw not_supported;
            }
        }

        private void Passive()
        {
            if (mood == Mood.Past) //past rules for passive
            {
                for (int i = 0; i < ai; i++) //everything before AIN ALFIEL should be dama
                {
                    changeVowel(i, Letter.dama);
                }
                changeVowel(ai, Letter.kasra);
            }
            else if (mood == Mood.nonPast || mood == Mood.nonPast_Maj || mood == Mood.nonPast_Man)
            {
                changeVowel(0, Letter.dama);
                //this[0].Vowel = Letter.dama;
                for (int i = 1; i <= ai; i++)
                    changeVowel(i, Letter.fatha);
            }
        }

        private void Hamza()
        {
            //set the right kursi
            for (int i =0;i<Count; i++)
            {
                changeCons(i, this[i].Cons); //this functions knows how to set the right kursi for alif, so if we Encounter an Elif this will set the right kursi.
            }
            //phonetic rules
            if (form != 8)//following rules are for every form except 8
            {
                for(int i = 0; i<Count;i++)
                {
                    if(Letter.isAlif(this[i].Cons) && i < Count -1 && 
                        Letter.isAlif(this[i+1].Cons) && 
                        this[i+1].Vowel == Letter.sukun) //in case of a two hamzas diphtong
                    {
                        //make the next letter be a "Mater Lectionis"
                        this[i + 1].Vowel = '0';
                        this[i + 1].Cons = Letter.getMaterLectionis(this[i].Vowel);
                    }
                }
            }

            //"shorted" imperative
            if (form == 1 &&
                mood == Mood.imperative &&
                (root.ToString() == "أكل" ||
                root.ToString() == "أخذ" ||
                root.ToString() == "أمر"))
            {
                RemoveRange(0, 2);
            }

            //form 8 root أخذ - exception to rules
            if (form == 8 && root.ToString() == "أخذ")
            {
                RemoveLetter(1);//remove fa alfiel
                this[1].IsShada = true; //add shada to ta.
            }

            //masdar form 2
            if (form == 2 && Letter.isAlif(root.L) && mood == Mood.infinitive)
            {
                InitVerb("تَفْعِلَة");
            }
        }
        private void FWFY()
        {
            //The phonetic rules
            if (fi > 0 && this[fi].Vowel == Letter.sukun && (this[fi -1].Vowel == Letter.dama || this[fi - 1].Vowel == Letter.kasra))
            {
                F.Vowel = '0'; //makae Fa Alfiel a "Mater Lectionis"
                F.Cons = Letter.getMaterLectionis(this[fi - 1].Vowel);
            }

            //form 8 special rules
            if (form == 8)
            {
                RemoveLetter(1); //remove Fa Alfiel
                this[1].IsShada = true;
            }
         
            //form 1 special rules
            if (form == 1 && root.F == 'و' && ( ((mood == Mood.nonPast || mood == Mood.nonPast_Maj || mood == Mood.nonPast_Man) && !passive) || mood == Mood.imperative ))
            {
                RemoveLetter(fi);
                if (mood == Mood.imperative)
                    RemoveLetter(0); //remove first alif - we don't need it...
            }
        }
        private void AWAY()
        {

            //infinitives in forms 4 and 10
            if (mood == Mood.infinitive)
            {
                if (form == 4)
                {
                    InitVerb("إِفَالَة");
                    setPostFix();
                }
                if (form == 10)
                {
                    InitVerb("إِسْتِفَالَة");
                    setPostFix();
                }
            }

            //forms not included in following rules
            if (form == 2 || form == 3 || form == 5 || form == 6) return;

            if (mood == Mood.infinitive && (form == 7 || form == 8))
            {
                A.Cons = 'ي';
                return;
            }

            //past passive rule
            if (mood == Mood.Past && passive)
            {
                this[ai - 1].Vowel = Letter.kasra;
            }

            //normal rules
            if (this.A.Vowel != Letter.sukun) // meaning there is a vowel on Ain AlFiel
            {
                if (this[ai - 1].Vowel == Letter.sukun)
                {
                    this[ai - 1].Vowel = A.Vowel;
                }
                if (this[ai - 1].Vowel != '0') //rules apply only after a "vowel", not "mater lectionis"
                {
                    A.Vowel = '0';
                    A.Cons = Letter.getMaterLectionis(this[ai - 1].Vowel);
                    if (this[ai + 1].Vowel == Letter.sukun)
                        RemoveLetter(ai);
                }

                //rules for form 1
                if (form == 1)
                {
                    //imperative - we don't need alif if Fa alfiel has a vowel
                    if (mood == Mood.imperative)
                    {
                        if (F.Vowel != Letter.sukun) RemoveLetter(0); //remove the alif - not needed.
                    }
                    //special past ruels for form 1
                    if (mood == Mood.Past)
                    {
                        if (L.Vowel == Letter.sukun)
                        {
                            if (root.A == 'و')
                                F.Vowel = Letter.dama;
                            if (root.A == 'ي')
                                F.Vowel = Letter.kasra;
                        }
                    }
                    //agent active form 1
                    if (mood == Mood.agentActive)
                    {
                        A.Cons = 'ئ';
                    }
                    //agent passive form 1
                    if (mood == Mood.agentPassive)
                    {
                        if (root.A == 'و')
                            InitVerb("مَفُول");
                        if (root.A == 'ي')
                            InitVerb("مَفِيل");
                        setPostFix();
                    }

                    //past passive
                    if (mood == Mood.Past && passive)
                    {
                        this[ai - 1].Vowel = Letter.kasra;
                    }
                }
                
            }
        }
        private void LWLY()
        {
            //making sure the input is legal regarding to form 1
            if (form == 1)
            {

                if (this[li].Cons == 'و')
                {
                    if(vowel != Letter.dama)
                    {
                        throw new Exception("Ilegal input. In form 1, if the last letter of the root is و, the vowel in the non past mood must be Dama. ");
                    }
                }

                else
                {
                    if (vowel == Letter.dama)
                    {
                        throw new Exception("Ilegal input. In form 1, if the last letter of the root is ي, the vowel in the non past mood cannot be Dama.");
                    }
                }
            }


            //the agents
            //rule 7 - active agent
            if ((mood == Mood.agentActive) && (pronoun_principle == Pronouns_Partciple.ms) || ((mood == Mood.infinitive) && (form == 5 || form == 6)))
            {
                if (ncase != 2)
                {
                    if (known)
                        this[Count - 1].Vowel = '0';
                    else
                    {
                        RemoveLetter(Count - 1);
                        this[Count - 1].Vowel = Letter.kastraten;
                    }
                   
                }
                return;
            }
            if ((mood == Mood.agentActive) && (pronoun_principle == Pronouns_Partciple.fp)) return; //special rule - female plural of agent active does not include in the following rules.

            //rule 8 + 9 - agent passive
            if (mood == Mood.agentPassive)
            {
                if(form == 1)
                {
                    RemoveLetter(ai + 1); //remove the 'و'
                    if (root.L == 'و')
                        A.Vowel = Letter.dama;
                    else if (root.L == 'ي')
                        A.Vowel = Letter.kasra;
                    L.IsShada = true;
                    return;
                }
                else
                {
                    if (pronoun_principle==Pronouns_Partciple.ms)
                    {
                        L.Cons = 'ى';
                        L.Vowel = '0';
                        if (known)
                            A.Vowel = Letter.fatha;
                        else
                            A.Vowel = Letter.fathaten;
                        if (this[Count-1].Cons == 'ا') RemoveLetter(Count-1);
                        return;
                    }
                        if (pronoun_principle == Pronouns_Partciple.fs)
                        {
                            L.Cons = 'ا';
                            L.Vowel = '0';
                            return;
                        }
                }
            }

            //rule 10 - infinitives 
            if (mood == Mood.infinitive)
            {
                if (form == 2)
                {
                    InitVerb("تَفْعِلَة");
                    setPostFix();
                    return;
                }
                if (form == 5 || form == 6) return; //did that on the agent active male singular rules
                //else...
                L.Cons = 'ء';
                return;
            }

            //rule 6 = LAM ALFIEL with sukun will fall //this rule is first because elseway the sukun will be vanished before and we wouldn't know it was there.
            if ((li + 1 == Count) && L.Vowel == Letter.sukun) //this is for imperative in Majzum 
            {
                RemoveLetter(li);
                return; //there is no need to continue with the rest of the rules.
            }

            //phonetic rules
            if ((L.Vowel == Letter.sukun) && (this[li-1].Vowel == Letter.kasra || this[li-1].Vowel == Letter.dama))
            {
                L.Vowel = '0'; //make Fa Alfiel a "Mater Lectionis"
                L.Cons = Letter.getMaterLectionis(this[li - 1].Vowel);
            }

            //rule 1 - for each form above 1L y <- w
            if (form >= 2) L.Cons = 'ي';

            //rule 2 - after matter lectionis remove LAM ALFIEL
            if (!(li + 1 == Count) /*LAM ALFIEL is NOT the last letter*/
                && (this[li + 1].Vowel == '0') && /* there is a matter lectionis after lam alfiel*/
                !isDual/*dual is NOT affected by the next rule*/) 
            {
                RemoveLetter(li);
                li -= 1; //so we won't mistably invoke rule No.4

                if (A.Vowel != Letter.fatha) //"sacred fatha" rule
                {
                    A.Vowel = Letter.getVowelFromMaterLectionis(this[li + 1].Cons); //match the vowel to the mater lectionis, wich is located in index li after the removal of LAM ALFIEL
                }
                else if (A.Vowel == Letter.fatha) //"sacred fatha" rule
                {
                    this[li + 1].Vowel = Letter.sukun;
                }
                else throw new Exception("There is a problem with LAWY rule number 2. Please contact developer for help.");
            }

            //rule 3 - past she form
            if (mood == Mood.Past && pronoun == Pronoun.she)
            {
                if (!(A.Vowel == Letter.kasra)) //the exception to the rule (happnes in passive form)
                {
                    RemoveLetter(li);
                    li -= 1; //so we won't mistably invoke rule No.4
                }
            }

            //rule 4
            if ((li + 1 == Count) && this[li-1].Vowel == Letter.fatha)
            {
                L.Cons = 'ى';
                L.Vowel = '0';

                if (form == 1 && root.L == 'و')
                {
                    L.Cons = 'ا';
                }
            }

            //rule 5
            if (mood == Mood.nonPast) //note: nonPast_Mantsub is an exception to this rule because of "sacred fatha" rule
            {
                if (li+1 == Count) //LAM ALFLIEL  is the last letter
                {
                    L.Vowel = '0'; 
                }
            }


            

        }
        private void Doubles()
        {
            if (form == 2 || form == 5) return; //not part of this special root type.

            //last letter lam alfiel with sukun - too options - not merging or adding fatha
            if (L.Vowel == Letter.sukun) 
            {
                if (Count == li + 1) // lam alfiel is the last letter
                {
                    if (option_index == 0)
                    {
                        isThereAnotherOption = true;
                        return;
                    }
                    if (option_index == 1)
                        L.Vowel = Letter.fatha;
                }
                else
                    return;
                
            }


            //L is with vowel - merging
            if (L.Cons == this[li-1].Cons) //lam alfiel beside ain alfiel - merging
            {
                if (this[li - 2].Vowel == Letter.sukun)
                {
                    this[li - 2].Vowel = A.Vowel;
                }
                RemoveLetter(li - 1);
                L.IsShada = true;
            }

            //after merging, we need to remove ALIF of form one:
            if (form == 1 && mood == Mood.imperative)
            {
                if (F.Vowel != Letter.sukun)
                    RemoveLetter(0);
            }

        }

        private void Form1()
        {
            switch(mood)
            {
                case Mood.Past:
                    InitVerb("فَعَل");
                    if(pastVowelKasra)
                    {
                        A.Vowel = Letter.kasra;
                    }
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َفْعَل", getEITN()));
                    A.Vowel = vowel;
                    break;
                case Mood.imperative:
                    InitVerb("إِفْعَل");
                    if (vowel == 'ُ' /*dama*/)
                    {
                        this[0].Cons = 'أ';
                        this[0].Vowel = 'ُ'; //dama
                    }
                    A.Vowel = vowel;
                    break;
                case Mood.agentActive:
                    InitVerb("فَاعِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مَفْعُول");
                    break;
                case Mood.infinitive:
                    throw new Exception("No single form, Check Dictionary");
                    break;
                default:
                    throw not_supported;
            }
       
        }
        private void Form2()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("فَعَّل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}ُفَعِّل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("فَعِّل");
                    break;
                case Mood.agentActive:
                    InitVerb("مُفَعِّل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مُفَعَّل");
                    break;
                case Mood.infinitive:
                    InitVerb("تَفْعِيل");
                    break;
                default:
                    throw not_supported;
            }

        }
        private void Form3()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("فَاعَل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}ُفَاعِل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("فَاعِل");
                    break;
                case Mood.agentActive:
                    InitVerb("مُفَاعِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مُفَاعَل");
                    break;
                case Mood.infinitive:
                    if (option_index == 0)
                    {
                        InitVerb("مُفَاعَلَة");
                        isThereAnotherOption = true;
                    }
                    if (option_index == 1)
                        InitVerb("فِعَال");
                    break;
                default:
                    throw not_supported;
            }

        }
        private void Form4()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("أَفْعَل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}ُفْعِل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("أَفْعِل");
                    break;
                case Mood.agentActive:
                    InitVerb("مُفْعِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مُفْعَل");
                    break;
                case Mood.infinitive:
                    InitVerb("إِفْعَال");
                    break;
                default:
                    throw not_supported;
            }
        }
        private void Form5()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("تَفَعَّل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َتَفَعَّل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("تَفَعَّل");
                    break;
                case Mood.agentActive:
                    InitVerb("مٌتَفَعِّل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مٌتَفَعَّل");
                    break;
                case Mood.infinitive:
                    InitVerb("تَفَعُّل");
                    break;
                default:
                    throw not_supported;
            }
        }
        private void Form6()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("تَفَاعَل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َتَفَاعَل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("تَفَاعَل");
                    break;
                case Mood.agentActive:
                    InitVerb("مٌتَفَاعِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مٌتَفَاعَل");
                    break;
                case Mood.infinitive:
                    InitVerb("تَفَاعُل");
                    break;
                default:
                    throw not_supported;
            }
        }
        private void Form7()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("إِنْفَعَل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َنْفَعِل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("إِنْفَعِل");
                    break;
                case Mood.agentActive:
                    InitVerb("مُنْفَعِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مُنْفَعَل");
                    break;
                case Mood.infinitive:
                    InitVerb("إِنْفِعَال");
                    break;
                default:
                    throw not_supported;
            }
        }
        private void Form8()
        {
            char ta = 'ت';
            int ti = 2; //index of ta in the verb.

            //changing of "ta" in form 8:
            switch(root.F)
            {
                case 'ص':
                case 'ض':
                    ta = 'ط';
                    break;
                case 'ز':
                    ta = 'د';
                    break;
            }

            switch (mood)
            {
                case Mood.Past:
                    InitVerb(string.Format("إِفْ{0}َعَل", ta));
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َفْ{1}َعِل", getEITN(), ta));
                    break;
                case Mood.imperative:
                    InitVerb(string.Format("إِفْ{0}َعِل", ta));
                    break;
                case Mood.agentActive:
                    InitVerb(string.Format("مُفْ{0}َعِل", ta));
                    break;
                case Mood.agentPassive:
                    InitVerb(string.Format("مُفْ{0}َعَل", ta));
                    break;
                case Mood.infinitive:
                    InitVerb(string.Format("إِفْ{0}ِعَال", ta));
                    break;
                default:
                    throw not_supported;
            }

            //merging of FA ALFIEL with the form's "ta"
            switch (root.F)
            {
                case 'ت':
                case 'ط':
                case 'د':
                case 'ذ':
                    this[fi].Vowel = this[ti].Vowel;
                    RemoveLetter(ti);
                    this[fi].IsShada = true;
                    break;
            }
        }
        private void Form9()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("إِفْعَلَل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َفْعَلِل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("إِفْعَلِل");
                    break;
                case Mood.agentActive:
                    InitVerb("مُفْعَلِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مُفْعَلَل");
                    break;
                case Mood.infinitive:
                    InitVerb("إِفْعِلَال");
                    break;
                default:
                    throw not_supported;
            }
        }
        private void Form10()
        {
            switch (mood)
            {
                case Mood.Past:
                    InitVerb("إِسْتَفْعَل");
                    break;
                case Mood.nonPast:
                case Mood.nonPast_Maj:
                case Mood.nonPast_Man:
                    InitVerb(string.Format("{0}َسْتَفْعِل", getEITN()));
                    break;
                case Mood.imperative:
                    InitVerb("إِسْتَفْعِل");
                    break;
                case Mood.agentActive:
                    InitVerb("مُسْتَفْعِل");
                    break;
                case Mood.agentPassive:
                    InitVerb("مُسْتَفْعَل");
                    break;
                case Mood.infinitive:
                    InitVerb("إِسْتِفْعَال");
                    break;
                default:
                    throw not_supported;
            }
        }


        //postfix functions
        private void setPostFix()
        {
            addPostfix(Postfix);
            
            //postvoweling for nouns
            if ((Mood)mood == Mood.agentActive ||
                (Mood)mood == Mood.agentPassive ||
                (Mood)mood == Mood.infinitive)
                addPostVowel();
        }
        private string Postfix
        {
            get
            {
                switch(mood)
                {
                    case Mood.Past:
                        return Postfix_past;
                    case Mood.nonPast:
                        return Postfix_nonPast;
                    case Mood.imperative:
                    case Mood.nonPast_Maj:
                    case Mood.nonPast_Man:
                        return Postfix_nonPast_short;
                    case Mood.agentActive:
                    case Mood.agentPassive:
                        return Postfix_Agent;
                    case Mood.infinitive:
                        return Postfix_Infinitive;
                    default:
                        throw invalid_input;

                }
            }
        }
        private string Postfix_past
        {
            get 
            { 
                switch((Pronoun)pronoun)
                {
                    case Pronoun.I:
                        return "ْتُ";
                    case Pronoun.ms_you:
                        return "ْتَ";
                    case Pronoun.fs_you:
                        return "ْتِ";
                    case Pronoun.he:
                        return "َ";
                    case Pronoun.she:
                        return "َتْ";
                    case Pronoun.we:
                        return "ْنَا";
                    case Pronoun.mp_you:
                        return "ْتُمْ";
                    case Pronoun.fp_you:
                        return "ْتُنَّ";
                    case Pronoun.m_they:
                        return "ُوا";
                    case Pronoun.f_they:
                        return "ْنَ";
                    default:
                        throw not_supported; 
                }
            }
        }
        private string Postfix_nonPast
        {
            get
            {
                switch(pronoun)
                {
                    case Pronoun.I:
                    case Pronoun.ms_you:
                    case Pronoun.he:
                    case Pronoun.she:
                    case Pronoun.we:
                        return "ُ";
                    case Pronoun.fs_you:
                        return "ِينَ";
                    case Pronoun.mp_you:
                    case Pronoun.m_they:
                        return "ُونَ";
                    case Pronoun.f_they:
                    case Pronoun.fp_you:
                        return "ْنَ";
                    default:
                        throw not_supported;

                }
            }
        }
        private string Postfix_nonPast_short
        {
            get
            {
                switch (pronoun)
                {
                    case Pronoun.I:
                    case Pronoun.ms_you:
                    case Pronoun.he:
                    case Pronoun.she:
                    case Pronoun.we:
                        if (mood == Mood.nonPast_Man)
                            return "َ";
                        else
                            return "ْ";
                    case Pronoun.fs_you:
                        return "ِي";
                    case Pronoun.mp_you:
                    case Pronoun.m_they:
                        return "ُوا";
                    case Pronoun.f_they:
                    case Pronoun.fp_you:
                        return "ْنَ";
                    default:
                        throw not_supported;

                }
            }
        }
        private string Postfix_Agent
        {
            get
            {
                switch (pronoun_principle)
                {
                    case Pronouns_Partciple.ms:
                        return "";
                    case Pronouns_Partciple.fs:
                        return "َة";
                    case Pronouns_Partciple.mp:
                        if (ncase == 1)
                            return "ُونَ";
                        else
                            return "ِينَ";
                    case Pronouns_Partciple.fp:
                        return "َات";
                    default:
                        throw invalid_input;
                }
            }
        }
        private void addPostVowel()
        {
            //this functions is just for noun forms. It adds the last vowel of the the noun according to the case of it
            if (pronoun_principle == Pronouns_Partciple.mp) return; //that case we don't need to add a vowel. the case is shown in the already added postfix. 

            //female plural rules
            if (ncase == 2 && pronoun_principle == Pronouns_Partciple.fp)
            {
                ncase = 3;
            }

            StringBuilder output = new StringBuilder();
            char[][] vowelsTable = new char[][] {new char[] {Letter.damaten, Letter.fathaten, Letter.kastraten}, 
                                                new char[] {Letter.dama, Letter.fatha, Letter.kasra}};
            output.Append(vowelsTable[Convert.ToInt32(known)][ncase - 1]);
            
            if (output[output.Length - 1] == Letter.fathaten && this[Count - 1].Cons != 'ة' && this[Count - 1].Cons != 'ء')
            {
                output.Append("ا");
            }
            addPostfix(output.ToString());
        }
        private string Postfix_Infinitive
        {
            get
            {
                switch (pronoun_principle)
                {
                    case Pronouns_Partciple.ms:
                        return "";
                    case Pronouns_Partciple.mp:
                        return "َات";
                    default:
                        throw invalid_input;
                }
            }
        }
        private string Postfix_dual
        {
            get
            {
                switch(mood)
                {
                    case Mood.Past:
                    case Mood.imperative:
                    case Mood.nonPast_Man:
                        return "َا";
                    case Mood.nonPast:
                    case Mood.nonPast_Maj:
                        return "َانِ";
                    default:
                        throw invalid_input;
                }
            }
        }
        //the root properties
        private Letter F
        {
            get { return this[fi]; }
        }
        private Letter A
        {
            get { return this[ai]; }
        }
        private Letter L
        {
            get { return this[li]; }
        }

        private void RemoveLetter(int i) //remove letter in position "i", and update indexes.
        {
            RemoveAt(i);
            updateIndex(ref fi, i);
            updateIndex(ref ai, i);
            updateIndex(ref li, i);
        }

        private void updateIndex(ref int index, int removedIndex) //update index of a letter when another one's removed
        {
            if (index > removedIndex)
            {
                index--;
            }
        }
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            
            //for majzum or mantsub:
            if ((Mood)mood == Mood.nonPast_Maj)
                 output.Append("لَم ");
            if ((Mood)mood == Mood.nonPast_Man)
                output.Append( "أَنْ ");

            //for nouns - adding "al"
            if (known)
            if ((Mood)mood == Mood.agentActive ||
                (Mood)mood == Mood.agentPassive ||
                (Mood)mood == Mood.infinitive)
            {
                output.Append("أَل");
                if (Letter.isAlshamsiaLetter(this[0].Cons))
                {
                    this[0].IsShada = true;
                }
                else
                    output.Append("ْ"); //add sukun to lam.
            }

            output.Append(base.ToString());

            if (mood == Mood.imperative) output.Append("!");

            return output.ToString();
        }

        
    } //end of class
}
