import styles from "./Report.module.scss";
import React from "react";
import {PdfReport} from "@/models";

interface ReportProps {
    report: PdfReport
}

function Report({report}: ReportProps): React.ReactNode {
    return (
        <div className={styles.report}>
            <p>{report.link}</p>
        </div>
    )
}

export default Report;