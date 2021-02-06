using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryEmotion : IEmotion
{
    private Hashtable controllers;

    public AngryEmotion(Hashtable controllers)
    {
        this.controllers = controllers;
    }

    public void Apply(float intensity)
    {
        GameObject cntr;

        /*
         * Emotii date pe fata - Paul Ekman, pag 205-206
         * "Femeia din stanga are doar sprancenele trase in jos si impreunate. Aceasta expresie in sine, 
         * fara ochii holbati, poate fi interpretata in mai multe feluri. Expresia este produsa de ceea
         * ce Darwin a numit muschiul dificultatii. El a observat, la fel ca mine, ca orice tip de
         * dificultate, mentala sau fizica, determina contractarea acestui muschi, tragand in jos si
         * impreunand sprancenele. Perplexitatea, confuzia, concentrarea, hotararea - toate pot fi
         * sugerate prin acest gest.
         */
        cntr = (GameObject) this.controllers["BrowIn_L_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, -0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["BrowIn_R_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, -0.025f * intensity, 0);

        /* 
         * Emotii date pe fata - Paul Ekman, pag 206
         * "Sa incepem cu pleoapele si sprancenele. In fotografia A, pleoapele inferioare si superioare 
         * sunt incordate. Poate fi un semn subtil al furiei controlare, sau doar o enervare usoara. 
         * Poate sa apara si cand nu exista niciun fel de enervare, dar persoana incearca literalmente 
         * sau la figurat sa fie atenta la ceva sau se concentreaza intens."
         *
         * Emotii date pe fata - Paul Ekman, pag 207
         * "Imaginea C poate sa fie de asemenea un semnal al furiei controlate sau al unei usoare enervari.
         * Poate aparea cand o persoana se simte usor perplexa, se concentreaza sau are o dificultate.
         * Despre care din aceste situatii e vorba depinde de context."
         *
         * Emotii date pe fata - Paul Ekman, pg 208
         * "Imaginea E reprezinta o actiune aditionala foarte importanta, ridicarea pleoapei de sus.
         * Aceasta este o cautatura; in acest caz nu e aproape niciun dubiu ca aceasta ar fi un semn de
         * furie, o furie controlata, probabil."
         */
        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(intensity * 0.01f, intensity * 0.025f, 0);
        
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(intensity * 0.01f, intensity * 0.025f, 0);

        cntr = (GameObject) this.controllers["UprLid_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, intensity * 0.025f, 0);

        cntr = (GameObject) this.controllers["UprLid_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, intensity * 0.025f, 0);

        /*
         * Emotii date pe fata - Paul Ekmanm, pg 209
         * "In imaginea I, ambele buze sunt apasate, ca in poza H si, in plus, buza de jos este impinsa 
         * in sus. Aceasta poate fi o furie controlata sau o resemnare, iar unele persoane folosesc asta
         * ca pe un semn ca se gandesc, in timp ce la alte persoane este un manierism frecvent. 
         * Presedintele Clinton avea frecvent aceasta miscare, ca manierism. In fotografia J, colturile
         * buzei sunt stranse, iar buza de jos este impinsa in sus. Cand acest lucru apare singular, cum 
         * e aici, creeaza ambiguitate; poate avea oricare dintre semnificatiile fotografiei I."
         * 
         * Emotii date pe fata - Paul Ekman, pg 210
         * "Ultima modalitate in care furia poate fii inregistrata in muschii gurii ati vazut-o la
         * demonstrantii canadieni si la Maxine Kenny; buza superioara este ridicata iar cea inferioara
         * lasata in jos, ambele fiind ingustate. Gura are o forma patratoasa."
         *
         * Emotii date pe fata - Paul Ekman, pg 208
         * "Acum sa ne uitam la semnalele din maxilar si buze. La manie, falca este adesea impinsa
         * inainte, ca in imaginea G"
         */

        // Impinge falca inainte 
        cntr = (GameObject) this.controllers["Jaw_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0, intensity * 0.025f);

        // Buzele devin apasate
        cntr = (GameObject) this.controllers["UprLip_2_cntr"];
        cntr.transform.localPosition += new Vector3(0, intensity * -0.025f, 0);
    }
}