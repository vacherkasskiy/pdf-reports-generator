import styles from "./ReportTask.module.scss";
import React, {useRef} from "react";
import {ReportModel, ReportStatus} from "@/models";
import {Label, MyButton} from "@/ui";
import {theme} from "@/ui/utils";
import copyIcon from "@/public/icons/copy_icon.png"
import {beautifyReportBody} from "@/utils";

interface ReportTaskProps {
    report: ReportModel
    labelTheme: theme
    labelText: string
    isExpanded: boolean
    onExpand: () => void
    onRegenerate: () => void
    onDelete: () => void
    onView: () => void
    onCopy: (event: React.MouseEvent) => void
}

function ReportTask(
    {
        report,
        labelTheme,
        labelText,
        isExpanded,
        onExpand,
        onRegenerate,
        onDelete,
        onView,
        onCopy
    }: ReportTaskProps): React.ReactElement {
    const reportBodyRef = useRef<HTMLDivElement>(null);
    const defaultReportBodyHeight = 100;

    const applyHeight = () => {
        const curr = reportBodyRef.current;

        if (curr)
        {
            if (curr.scrollHeight <= defaultReportBodyHeight) return;
            if (isExpanded) curr.style.height = `${curr.scrollHeight + 5}px`;
            else curr.style.height = `${defaultReportBodyHeight}px`;
        }
    }

    applyHeight();

    return (
        <div className={styles.report}>
            <div className={styles.info}>
                <p className={styles.id}>
                    ID:
                    <span className={styles.value}>{report.id}</span>
                </p>
                <Label size={'s'} text={labelText} theme={labelTheme} type={'outline'} />
            </div>
            <div ref={reportBodyRef} className={styles.reportBody} onClick={onExpand}>
                <img
                    src={copyIcon.src}
                    className={styles.copyIcon}
                    alt="Copy text"
                    onClick={onCopy}
                />
                <pre className={styles.text}>{beautifyReportBody(report.reportBody)}</pre>
            </div>
            <div className={styles.buttons}>
                <MyButton
                    onClick={onView}
                    disabled={report.link == null}
                    text={"View"}
                    theme={'blue'}
                />
                <MyButton
                    text={"Generate again"}
                    theme={'orange'}
                    onClick={onRegenerate}
                    disabled={![ReportStatus.Error, ReportStatus.Ready].includes(report.status)}
                />
                <MyButton
                    text={"Delete"}
                    theme={'red'}
                    onClick={onDelete}
                />
            </div>
        </div>
    )
}

export default ReportTask;