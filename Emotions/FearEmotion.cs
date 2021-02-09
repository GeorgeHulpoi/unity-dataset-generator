using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearEmotion : IEmotion
{
    private Hashtable controllers;

    public FearEmotion(Hashtable controllers)
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

        // Pe axa Y, ia valori intre [-0.025, 0] (tensionarea pleoapei superioare),
        // unde minimul este pleoapa lasata, iar  maximul este pleoapa ridicata
        // Pe axa X, ia valori intre [0, 0.025] (tensionarea pleoapei inferioare)
        // unde minimul este pleoapa neutra, iar maximul este ploapa tensionata
        cntr = (GameObject) this.controllers["EyeSqz_R_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["EyeSqz_L_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, 0.025f * intensity, 0);

        // Ridicam pleoapa superioara in sus
        // Axa Y ia valori intre [-0.025, 0.025], unde minimum este pleoapa acopera tot ochiul,
        // iar maximum este ridicarea pleoapei total. Valoarea 0 reprezinta o pozitie neutra.
        cntr = (GameObject) this.controllers["UprLid_R_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["UprLid_L_cntr"];
        cntr.transform.localPosition += new Vector3(0, 0.025f * intensity, 0);

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
        cntr.transform.localPosition += new Vector3(0.025f * intensity, 0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["BrowIn_L_cntr"];
        cntr.transform.localPosition += new Vector3(0.025f * intensity, 0.025f * intensity, 0);

        /*
         * Emotii date pe fata - Paul Ekman, pg 243
         * "Sa examinam acum indiciile de surpriza si frica din partea de jos a fetei.
         * In surpriza, falca este cazuta, ca in imaginea K, iar in frica buzele sunt
         * trase in spate, spre ochi, ca in imaginea L."
         */
        // Pe axa X ia valori intre [-0.025, 0.025], unde minimul este coltul gurii tras
        // catre ochi, iar maximul este un fel de buza tuguiata
        // Pe axa Y ia valori intre [-0.025, 0], unde minimul este coltul gurii tras in jos
        // iar maximul este pozitia neutra (face sa fie un zambet in mod neutru)
        cntr = (GameObject) this.controllers["Crnr_R_2_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, -0.025f * intensity, 0);
        cntr = (GameObject) this.controllers["Crnr_L_2_cntr"];
        cntr.transform.localPosition += new Vector3(-0.025f * intensity, -0.025f * intensity, 0);

    }
}