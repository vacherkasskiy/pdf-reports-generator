import {ReportModel} from "@/models";
import React from "react";
import styles from "./ReportsPage.module.scss";
import ReportsList from "@/components/ReportsPage/ReportsList/ReportsList";
import NewReportContainer from "@/components/ReportsPage/NewReportInput/NewReportContainer";

interface ReportsPageProps {
    reports: ReportModel[]
    onRegenerate: (reportId: string) => void
    onDelete: (reportId: string) => void
}

function ReportsPage({reports, onRegenerate, onDelete}: ReportsPageProps): React.ReactElement {
    return (
        <div className={styles.reportsPage}>
            <div className={styles.newReportInputContainer}>
                <NewReportContainer />
            </div>
            <div className={styles.reportsList}>
                <ReportsList
                    reports={reports}
                    onDelete={onDelete}
                    onRegenerate={onRegenerate}
                />
            </div>
        </div>
    )
}

export default ReportsPage