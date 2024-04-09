interface ReportModel {
    id: string,
    status: Status,
    link: string | undefined
}

enum Status {
    Waiting,
    InProgress,
    Ready,
    Error
}

export default ReportModel;