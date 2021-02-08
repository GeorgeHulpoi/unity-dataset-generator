using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseEmotion : IEmotion
{
    private Hashtable controllers;

    public SurpriseEmotion(Hashtable controllers)
    {
        this.controllers = controllers;
    }

    public void Apply(float intensity)
    {
        GameObject cntr;

        /*
         * Emotii date pe fata - Paul Ekman, pg 240
         * "Ochii sunt esentiali atat pentru surpriza, cat si pentru frica, dar si pentru a distinge cele
         * doua emotii intre ele. In imaginea A, pleoapele superioare au fost ridicate doar putin in
         * comparatie cu fata neutra reprezentata in imaginea B. Ar putea fi un semn de surpriza, dar
         * probabil e un semn de atentie sau de interes. In imaginea C, pleoapele superioare sunt
         * ridicate mai mult, fiind acum foarte probabil vorba fie de surpriza, fie despre ingrijorare, fie
         * despre frica - depinde de ce se intampla pe restul chipului."
         *
         * Emotii date pe fata - Paul Ekman, pg 240
         * "In acest caz, indiciul care ne spune ca nu este vorba despre atentie sau surpriza (ci de frica)
         * trebuie cautat in pleoapele inferioare. Cand pleoapele inferioare tensionate insotesc pleoapele
         * superioare in ridicare, iar restul fetei este neschimbat, vorbim aproape intodeauna despre un
         * indiciu al fricii."
         *
         * Emotii date pe fata - Paul Ekman, pg 244
         * "Imaginea O arata cat de importante sunt pleoapele superioare ridicate in semnalizarea fricii.
         * Desi pleoapele de jos nu sunt tensionate, iar actiunile sprancenelor si ale gurii sunt de obicei
         * mai vizibile in surpriza, pleoapele superioare sunt atat de ridicate in aceasta fotografie,
         * incat creeaza impresia de frica."
         */

        // Pe axa Y, ia valori intre [-0.025, 0] (tensionarea pleoapei superioare)
        // In cazul in care este -0.025, adaug 0.025 ca sa-l duc la extrema opusa
        // Pe axa X, ia valori intre [0, 0.025] (tensionarea pleoapei inferioare)
        // In cazul in care este 0.025, scad cu 0.025 ca sa detensionez pleoapa inf
        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        // Ridicam pleoapa superioara in sus
        // Axa Y ia valori intre [-0.025, 0.025], unde minimum este pleoapa acopera tot ochiul,
        // iar maximum este ridicarea pleoapei total. Valoarea 0 reprezinta o pozitie neutra.
        cntr = (GameObject) this.controllers["UprLid_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.0075f * intensity, 0);
        cntr = (GameObject) this.controllers["UprLid_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.0075f * intensity, 0);

        /*
         * Emotii date pe fata - Paul Ekman, pg 242
         * "Foarte rar sprancenele ridicate vor fi un semn de surpriza fara ca pleoapele sa fie si ele
         * ridicate."
         *
         * Emotii date pe fata - Paul Ekman, pg 243
         * "Stim ca poza I arata surpriza mai degraba decat frica pentru ca plaoepele inferioare nu
         * sunt tensionate, iar sprancenele nu se impreuna, desi sunt ridicate; ambele semne sunt insa
         * evidente in poza J."
         */
        // Pe axa Y ia valori intre [-0.025, 0.025], unde minimul este spranceana este in jos,
        // iar maximul este in sus.
        // Pe axa X ia valori intre [0, 0.025] unde minimul este neutru, iar maximul este
        // spranceana impreunata cu cealalta
        cntr = (GameObject) this.controllers["BrowIn_R_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["BrowIn_L_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, 0.025f * intensity, 0);

        /*
         * Emotii date pe fata - Paul Ekman, pg 243
         * "Sa examninam acum indiciile de surpriza si frica din partea de jos a fetei.
         * In surpriza, falca este cazuta, ca in imaginea K, iar in frica buzele sunt
         * trase in spate, spre ochi, ca in imaginea L."
         */
        // Pe axa Y ia valori intre [-0.025, 0], unde minimul este gura deschisa larg,
        // iar maximul este gura inchisa
        // Pentru a nu exagera cu gura deschisa, am ales ca -0.0140 sa fie minimul in cazul
        // acestei emotii
        cntr = (GameObject) this.controllers["Jaw_cntr"];
        cntr.transform.localPosition += new Vector3(0, -0.014f * intensity, 0);
    }
}