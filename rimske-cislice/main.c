#include <stdio.h>
#include <string.h>

const struct {
    int value;
    const char *symbol;
} romanMap[] = {
    {1000, "M"},
    {900,  "CM"},
    {500,  "D"},
    {400,  "CD"},
    {100,  "C"},
    {90,   "XC"},
    {50,   "L"},
    {40,   "XL"},
    {10,   "X"},
    {9,    "IX"},
    {5,    "V"},
    {4,    "IV"},
    {1,    "I"}
};

const int romanMapSize = (sizeof(romanMap) / sizeof(romanMap[0]));


void arabicToRoman(int arabic, char* roman) {
    int position = 0;
    for (int i = 0; i < romanMapSize; i++) {
        while(arabic >= romanMap[i].value) {
            strncpy(roman + position, romanMap[i].symbol, strlen(romanMap[i].symbol));
            position += strlen(romanMap[i].symbol);
            
            arabic -= romanMap[i].value;
        }
    }
    roman[position] = '\0';
}

void romanToArabic(const char* roman, int* arabic) {
    int pos = 0;
    *arabic = 0;

    while (roman[pos] != '\0') {
        for (int j = 0; j < romanMapSize; j++) {
            int len = strlen(romanMap[j].symbol);

            if (strncmp(roman + pos, romanMap[j].symbol, len) == 0) {
                *arabic += romanMap[j].value;
                pos += len;
                break;
            }
        }
    }
}


int main()
{
    int arabic;
    char roman[20];
    printf("---------------------------\n");
    printf("enter arabic: ");
    scanf("%d", &arabic);
    arabicToRoman(arabic, roman); 
    printf("roman: %s\n", roman);
    
    printf("---------------------------\n");
    printf("enter roman: ");
    scanf("%s", &roman);
    romanToArabic(roman, &arabic);
    printf("arabic: %d\n", arabic);

    printf("---------------------------\n");
    return 0;
}
