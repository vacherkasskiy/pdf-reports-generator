import React from "react";
import styles from "./ReportsList.module.scss";
import ReportTask from "@/components/ReportsPage/Report/ReportTask";
import {ReportModel} from "@/models";

interface ReportsListProps {
    reports: ReportModel[]
    onRegenerate: (reportId: string) => void
    onDelete: (reportId: string) => void
}

function ReportsList({reports, onRegenerate, onDelete}: ReportsListProps): React.ReactElement {
    return (
        <div className={styles.reportsList}>
            {reports.map((report, i) =>
                <ReportTask
                    key={i}
                    report={report}
                    onRegenerate={() => onRegenerate(report.id)}
                    onDelete={() => onDelete(report.id)}
                />
            )}
        </div>
    )
}

export default ReportsList;