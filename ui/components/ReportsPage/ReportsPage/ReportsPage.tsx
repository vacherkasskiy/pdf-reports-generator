import {ReportModel} from "@/models";
import React from "react";
import styles from "./ReportsPage.module.scss";
import ReportsList from "@/components/ReportsPage/ReportsList/ReportsList";

interface ReportsPageProps {
    reports: ReportModel[]
}

function ReportsPage({reports}: ReportsPageProps): React.ReactElement {
    return (
        <div className={styles.reportsPage}>
            <div className={styles.reportsList}>
                <ReportsList reports={reports} />
            </div>
        </div>
    )
}

export default ReportsPage