export interface RequestResult<T> {
    success: boolean;
    successOrOnlyWarnings: boolean;
    messages: SimpleMessage[];
    data: T;
}

export interface SimpleMessage {
    header: string;
    description: string;
    isSimpleMessage: boolean;
    isError: boolean;
    isWarning: boolean;
    isWarningOrSimpleMessage: boolean;
    type: MessageType;
}

export enum MessageType {
    Simple = 0,
    Warning = 10,
    Error = 20,
}
