package orgnummer.orgnummer;

import references.references.DoubleReference;

import static numbers.StringToDecimal.StringToDecimal.getDecimalFromSingleDecimalDigit;

public class orgnummer {
    public static boolean erGyldigOrgNummer(char [] orgnummer){
        boolean gyldig;
        double d1, d2, d3, d4, d5, d6, d7, d8, d9;
        double sum, rest, kontrollsiffer;
        DoubleReference failures;

        if(orgnummer.length == 9d){
            failures = new DoubleReference();
            failures.doubleValue = 0d;

            d1 = charToNumber(orgnummer[0], failures);
            d2 = charToNumber(orgnummer[1], failures);
            d3 = charToNumber(orgnummer[2], failures);
            d4 = charToNumber(orgnummer[3], failures);
            d5 = charToNumber(orgnummer[4], failures);
            d6 = charToNumber(orgnummer[5], failures);
            d7 = charToNumber(orgnummer[6], failures);
            d8 = charToNumber(orgnummer[7], failures);
            d9 = charToNumber(orgnummer[8], failures);

            if(failures.doubleValue == 0d) {
                sum = d1 * 3d + d2 * 2d + d3 * 7d + d4 * 6d + d5 * 5d + d6 * 4d + d7 * 3d + d8 * 2d;
                rest = sum % 11d;
                kontrollsiffer = 11d - rest;

                if (rest != 1d && kontrollsiffer == d9) {
                    gyldig = true;
                } else {
                    gyldig = false;
                }
            } else {
                gyldig = false;
            }
        }else{
            gyldig = false;
        }

        return gyldig;
    }

    public static double charToNumber(char c, DoubleReference failures){
        double val;

        // TODO: Make function return success or failure.
        val = getDecimalFromSingleDecimalDigit(c, 10d);

        if(val > 0d && val <= 9d){
        }else{
            failures.doubleValue = failures.doubleValue + 1d;
        }

        return val;
    }
}
