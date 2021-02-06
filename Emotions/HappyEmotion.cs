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
         * ca apar atunci cand oamenii nu simt niciun fel de bucirie, ca in politete, de plida."
         */
        cntr = (GameObject) this.controllers["Crnr_R_2_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0, 0);
        cntr = (GameObject) this.controllers["Crnr_L_2_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0, 0);

        cntr = (GameObject) this.controllers["Crnr_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Crnr_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        // Tragem usor buza usor spre exterior pentru a evita un bug vizual 
        cntr = (GameObject) this.controllers["UprLip_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0, 0.0125f * intensity);

        /*
         * Emotii date pe fata - Paul Ekman, pg 295
         * "Cu mai bine de o suta de ani in urma, marele neurolog francez Duchenne de Boulogne a descoperit
         * cum adevaratul zambet de bucirie difera de toate cele care nu au legatura cu placerea. El a studiat
         * modul in care fiecare muschi facial modifica expresia subiectilor, stimuland electric diferite parti
         * ale figurii si fotografiind rezultatul contractiilor musculare."
         *
         * Emotii date pe fata - Paul Ekman, pg 295-296
         * "Duchemme a scris: "Emotia bucuriei autentice se exprima prin contractia simultana a muschiului
         * zigomatic major si a muschilor orbiculari. Primul se supune vointei, dar al doilea e miscat de
         * dulcile emotii ale sufletului [retineti, scria in 1862]; (...) bucuria falsa, rasul amagitor nu
         * pot provoca si contractia celei de-a doua categorii de muschi... Muschii din jurul ochilor nu se
         * supun vointei; sunt miscati doar de un sentiment adevarat, de o emotie agreabila. Inertia lor,
         * in zambetm tradeaza un fals prieten."
         */

        // Laba gastei si punga de sub ochi
        cntr = (GameObject) this.controllers["Cheek_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Cheek_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);

        // Spranceana este trasa usor in jos (doar jumatate)
        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.0125f * intensity, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.0125f * intensity, 0);
    }
}