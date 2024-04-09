import {PdfReport} from "@/models";
import React from "react";

interface ReportsPageProps {
    reports: PdfReport[]
}

function ReportsPage({reports}: ReportsPageProps): React.ReactNode {
    return (
        <>
            <div>Reports: </div>
            {reports.map((report, i) =>
                <div key={i}>
                    {report.id}
                    {report.status}
                    {report.link ?? "Not ready"}
                </div>
            )}
        </>
    )
}

export default ReportsPage