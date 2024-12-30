interface ReportModel {
    id: string,
    status: Status,
    authorName: string,
    reportName: string,
    reportS3Link: string | undefined,
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