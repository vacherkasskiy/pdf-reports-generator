import React from "react";
import styles from "./ReportsList.module.scss";
import ReportTask from "@/components/ReportsPage/Report/ReportTask";
import {ReportModel} from "@/models";

interface ReportsListProps {
    reports: ReportModel[]
}

function ReportsList({reports}: ReportsListProps): React.ReactElement {
    return (
        <div className={styles.reportsList}>
            {reports.map((report, i) =>
                <ReportTask
                    key={i}
                    report={report}
                />
            )}
        </div>
    )
}

export default ReportsList;