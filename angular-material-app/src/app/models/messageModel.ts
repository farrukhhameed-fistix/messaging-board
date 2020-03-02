export class MessageModel{
    constructor(public content: string, public from: string, public time: any){

    }

    getUserInitial():string{
     return this.from && this.from.length > 0 ? this.from.charAt(0).toUpperCase() : '';   
    }
}