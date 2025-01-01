import {ReportModel} from "@/models";
import React from "react";
import styles from "./ReportsPage.module.scss";
import NewReportContainer from "@/components/ReportsPage/NewReportInput/NewReportContainer";
import {ReportsListContainer} from "@/components/ReportsPage/ReportsList/ReportsListContainer";

interface ReportsPageProps {
    reports: ReportModel[]
}

function ReportsPage({reports}: ReportsPageProps): React.ReactElement {
    return (
        <div className={styles.reportsPage}>
            <div className={styles.newReportInputContainer}>
                <NewReportContainer />
            </div>
            <div className={styles.reportsList}>
                <ReportsListContainer
                    reports={reports}
                />
            </div>
        </div>
    );
}

export default ReportsPage;