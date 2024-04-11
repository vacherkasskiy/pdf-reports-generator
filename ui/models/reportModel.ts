interface ReportModel {
    id: string,
    status: Status,
    link: string | undefined,
    reportBody: string,
    createdAt: string,
    updatedAt: string,
}

export enum Status {
    Waiting,
    InProgress,
    Ready,
    Error
}

export default ReportModel;