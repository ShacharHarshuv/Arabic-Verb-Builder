using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArabicVerbBuilder;

namespace ArabicVerbBuilderCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            try
            {
                //input variables:
                string _form = args[0];
                string _root = args[1];
                string _mood = args[2];
                string _pronoun = args[3];
                string _passive = args[4];
                string _vowel = args[5];
                string _pastVowelKasra = args[6];
                string _ncase = args[7];
                string _known = args[8];

                /// parse input:
                //form:
                int form = int.Parse(_form);
                //root
                Root root = new Root(_root);
                //mood
                Mood mood;
                switch (_mood)
                {
                    case "past":
                        mood = Mood.Past;
                        break;
                    case "non-past":
                        mood = Mood.nonPast;
                        break;
                    case "non-past-maj":
                        mood = Mood.nonPast_Maj;
                        break;
                    case "non-past-man":
                        mood = Mood.nonPast_Man;
                        break;
                    case "imperative":
                        mood = Mood.imperative;
                        break;
                    case "agent-active":
                        mood = Mood.agentActive;
                        break;
                    case "agent-passive":
                        mood = Mood.agentPassive;
                        break;
                    case "infinitive":
                        mood = Mood.infinitive;
                        break;
                    default:
                        throw new Exception("unrecognized mood name");
                }
                //pronoun
                Pronoun pronoun;
                Pronouns_Partciple pronoun_participle = Pronouns_Partciple.ms; //default
                switch(_pronoun){
                    case "I":
                        pronoun = Pronoun.I;
                        break;
                    case "ms_you":
                        pronoun = Pronoun.ms_you;
                        pronoun_participle = Pronouns_Partciple.ms;
                        break;
                    case "fs_you":
                        pronoun = Pronoun.fs_you;
                        pronoun_participle = Pronouns_Partciple.fs;
                        break;
                    case "he":
                        pronoun = Pronoun.he;
                        break;
                    case "she":
                        pronoun = Pronoun.she;
                        break;
                    case "d_you":
                        pronoun = Pronoun.d_you;
                        break;
                    case "md_they":
                        pronoun = Pronoun.md_they;
                        break;
                    case "we":
                        pronoun = Pronoun.we;
                        break;
                    case "mp_you":
                        pronoun = Pronoun.mp_you;
                        pronoun_participle = Pronouns_Partciple.mp;
                        break;
                    case "fp_you":
                        pronoun = Pronoun.fp_you;
                        pronoun_participle = Pronouns_Partciple.fp;
                        break;
                    case "m_they":
                        pronoun = Pronoun.m_they;
                        break;
                    case "f_they":
                        pronoun = Pronoun.f_they;
                        break;
                    default:
                        throw new Exception(_pronoun + " is not recognized as a pronoun");
                }
                //passive
                bool passive = parseBool(_passive);
                //vowel
                char vowel;
                switch (_vowel) {
                    case "kasra":
                        vowel = 'ِ'; //kasra
                        break;
                    case "damma":
                        vowel = 'ُ';
                        break;
                    case "fatha":
                        vowel = 'َ';
                        break;
                    default:
                        throw new Exception(_vowel + " is not recognized as a vowel");
                }
                //pastVowelKasra
                bool pastVowelKasra = parseBool(_pastVowelKasra);
                //ncase
                int ncase = int.Parse(_ncase);
                //known
                bool known = parseBool(_known);

                Console.WriteLine("success");
                ///if all arguments parsed succesfully, build the verb.
                Verb_Container verb = new Verb_Container(form, root, mood, pronoun, pronoun_participle, passive, vowel, pastVowelKasra, ncase, known);
                //print result
                Console.WriteLine("result: ");
                Console.WriteLine(verb.ToString());
            }
            catch (FormatException e)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("Make sure integer values are passed as integers.");
            }
            catch (IndexOutOfRangeException e){
                Console.WriteLine("ERROR");
                Console.WriteLine("Not enough arguments. Please pass 9 arguments.");
            }
            catch (Exception e)
            {
                Console.WriteLine("input error");
                Console.WriteLine(e.Message);
            }
            
        }

        static bool parseBool(string b)
        {
            switch (b) {
                case "true": return true;
                case "false": return false;
            }
            throw new Exception(b + " is not a boolean value");
        }
    }
}
