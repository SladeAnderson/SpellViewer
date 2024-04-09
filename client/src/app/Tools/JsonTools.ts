export class JsonTools {
    // class vars

    constructor(){}
    

    public Stringify(toString: object): string{
        console.info(`Stringifying: ${toString}`);

        return JSON.stringify(toString);
    }

    public  objectify(toObject: any ){
        console.info(`Parsing: ${toObject}`);

        return JSON.parse(toObject)
    }
}