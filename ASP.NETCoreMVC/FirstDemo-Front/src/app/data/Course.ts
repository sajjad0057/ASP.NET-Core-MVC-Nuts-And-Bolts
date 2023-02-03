import { ICourse } from "./ICourse";

export class Course implements ICourse{
    public name : string = "";
    public fees : number = 0;


    // by this constructor syntax to easily initialize current object 
    public constructor(init?:Partial<Course>){
        Object.assign(this,init);
    }
}