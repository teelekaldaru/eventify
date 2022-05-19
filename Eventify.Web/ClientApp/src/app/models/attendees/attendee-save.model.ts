export class AttendeeSave {
    id?: string;
    paymentMethod?: string;
    additionalInformation?: string;
    person?: PersonSave;
    company?: CompanySave;
}

export class PersonSave {
    firstName?: string;
    lastName?: string;
    personalCode?: string;
}

export class CompanySave {
    name?: string;
    registerCode?: string;
}

export enum AttendeeType {
    Unknown = 0,
    Person = 10,
    Company = 20
}
