import React from "react";
import styles from "./ReportsList.module.scss";
import {ReportModel} from "@/models";
import ReportTaskContainer from "@/components/ReportsPage/Report/ReportTaskContainer";

interface ReportsListProps {
    reports: ReportModel[]
}

function ReportsList({reports}: ReportsListProps): React.ReactElement {
    return (
        <div className={styles.reportsList}>
            {reports.map((report, i) =>
                <ReportTaskContainer
                    key={i}
                    report={report}
                />
            )}
        </div>
    )
}

export default ReportsList;