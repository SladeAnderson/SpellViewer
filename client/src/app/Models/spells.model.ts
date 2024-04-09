export interface spell {
    Casting_Time:string,
    Classes:string[],
    components: components,
    description: string,
    duration: string,
    level:string,
    name:string,
    range:string,
    is_Ritual:boolean,
    school:string,
    type:string
}

interface components {
    is_material:boolean,
    materials_needed?: string[],
    is_somatic:boolean,
    is_verbal: boolean
}