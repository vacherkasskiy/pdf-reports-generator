const beautifyReportBody = (reportBody: string): string => {
    return JSON.stringify(JSON.parse(reportBody), null, 4);
}

export default beautifyReportBody;