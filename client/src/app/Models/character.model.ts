import { spell } from "./spells.model";

export interface character {
    name: string,
    race: string,
    characterClass: string,
    knownSpells: IDspell[],
};

interface IDspell {
    name: string
};


