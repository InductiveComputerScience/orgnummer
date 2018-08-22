package tests;

import static orgnummer.orgnummer.orgnummer.erGyldigOrgNummer;

public class tests {
    public static double test(){
        double failures;
        boolean gyldig;

        failures = 0d;

        gyldig = erGyldigOrgNummer("913965639".toCharArray());

        if(!gyldig){
            failures = failures + 1d;
        }

        gyldig = erGyldigOrgNummer("913965638".toCharArray());

        if(gyldig){
            failures = failures + 1d;
        }

        return failures;
    }
}
