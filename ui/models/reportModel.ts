interface ReportModel {
    id: string,
    status: Status,
    link: string | undefined,
    reportBody: string,
    createdAt: Date,
    updatedAt: Date,
}

enum Status {
    Waiting,
    InProgress,
    Ready,
    Error
}

export default ReportModel;