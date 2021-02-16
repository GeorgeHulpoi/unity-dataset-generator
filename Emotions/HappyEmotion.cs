using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HappyEmotion : IEmotion
{
    private Hashtable controllers;

    public HappyEmotion(Hashtable controllers)
    {
        this.controllers = controllers;
    }

    public void Apply(float intensity)
    {
        GameObject cntr;
        
        /*
         * Emotii date pe fata - Paul Ekman, pg 293-294
         * "Este evident, chiar si privind in fuga fotografiile din acest capitol, ca zambetul este semnul
         * facial clasic al emotiilor agreabile. Amuzamentul, fiero, nakhes, multumirea, exaltarea,
         * placerile senzoriale, eliberarea, mirarea, Schadenfreude, extazul si, probabil, elevatia si
         * recunostinta, toate acestea implica zambetul. Zambetele corespunzatoare difera in intensitate,
         * in rapiditatea cu care apar si in functie de timpul cat sunt prezente pe chip.
         * Daca aceste diverse emotii ale placerii au ca element comun zambetul, atunci cum putem stii
         * exact care din ele este simtita de o alta persoana? Cercetari recente, pe care le-am mentionat in
         * capitolul 4, sustin intuitia mea potrivit careia nu chipul, ci vocea furnizeaza semnalele de
         * diferentiere intre emotiile placute."
         *
         * Emotii date pe fata - Paul Ekman, pg 294
         * "Zambetele pot crea confuzie nu doar pentru ca apar odata cu fiecare emotie placuta, ci si pentru
         * ca apar atunci cand oamenii nu simt niciun fel de bucurie, ca in politete, de plida."
         */

        // Ridicarea buzelor
        // Pe axa Y ia valori intre [-0.025, 0.025], unde valoarea minima reprezinta
        // buzele trase in jos, 0 reprezinta o valoare neutra, iar maximul reprezinta
        // buzele trase in sus
        // Scalarea se va face in intervalul [0.0, 0.025]
        cntr = (GameObject) this.controllers["Mouth_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);


        // Efectul de zambet, doar ca cu gura deschisa
        // Pe axa X, ia valori intre [-0.025, 0.025], unde minimul este buza trasa spre
        // exterior, iar maximul este buza facuta tuguiata
        // Pe axa Y, ia valori intre [-0.025, 0.025], doar ca intre [0.025, 0] nu se vede
        // nici o schimare, in schimb intre [0, 0.025], minimul este pozitie neutra,
        // iar maximul este coltul exterior tras in sus
        // In ambele cazuri, formula de scalare o sa fie egala cu intensity * 0.025
        cntr = (GameObject) this.controllers["Crnr_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Crnr_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        /*
         * Emotii date pe fata - Paul Ekman, pg 295
         * "Cu mai bine de o suta de ani in urma, marele neurolog francez Duchenne de Boulogne a descoperit
         * cum adevaratul zambet de bucurie difera de toate cele care nu au legatura cu placerea. El a studiat
         * modul in care fiecare muschi facial modifica expresia subiectilor, stimuland electric diferite parti
         * ale figurii si fotografiind rezultatul contractiilor musculare."
         *
         * Emotii date pe fata - Paul Ekman, pg 295-296
         * "Duchemme a scris: "Emotia bucuriei autentice se exprima prin contractia simultana a muschiului
         * zigomatic major si a muschilor orbiculari. Primul se supune vointei, dar al doilea e miscat de
         * dulcile emotii ale sufletului [retineti, scria in 1862]; (...) bucuria falsa, rasul amagitor nu
         * pot provoca si contractia celei de-a doua categorii de muschi... Muschii din jurul ochilor nu se
         * supun vointei; sunt miscati doar de un sentiment adevarat, de o emotie agreabila. Inertia lor,
         * in zambet tradeaza un fals prieten."
         */

        // Laba gastei si punga de sub ochi
        // Ia valori pe axa Y intre [0.0, 0.025], unde minimul este neutru, iar 
        // maximul este incordat
        cntr = (GameObject) this.controllers["Cheek_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Cheek_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);

        // Muschii orbitali sunt incordato
        // Pe axa Y ia valori intre [-0.025, 0], unde minimul reprezinta muschii
        // complet incordati, iar 0 este o pozitie neutra
        // Pentru ca nu trebuie incordati complet, o sa alegem ca valoarea maxima sa fie -0.011

        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.011f * intensity, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.011f * intensity, 0);

        // Deschidem putin gura pentru a da efectul de zambet
        // Pe axa Y ia valori intre [-0.025, 0], unde minimul reprezinta
        // gura larg deschisa, iar 0 pozitia neutra
        // Maximul ales este -0.0082, care reprezinta un zambet

        cntr = (GameObject) this.controllers["Jaw_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.0082f * intensity, 0);
    }
}