import styles from "./ReportTask.module.scss";
import React, {useRef, useState} from "react";
import {ReportModel, ReportStatus} from "@/models";
import {Label, MyButton} from "@/ui";
import {theme} from "@/ui/utils";
import copyIcon from "@/public/icons/copy_icon.png"

interface ReportTaskProps {
    report: ReportModel
    onRegenerate: () => void
    onDelete: () => void
}

function ReportTask({report, onRegenerate, onDelete}: ReportTaskProps): React.ReactElement {
    const getLabelText = (): string => {
        switch (report.status) {
            case 0:
                return 'WAITING';
            case 1:
                return 'IN PROGRESS';
            case 2:
                return 'READY';
            default:
                return 'ERROR';
        }
    }

    const getLabelTheme = (): theme => {
        switch (report.status) {
            case ReportStatus.Waiting:
                return 'dark';
            case ReportStatus.InProgress:
                return 'blue';
            case ReportStatus.Ready:
                return 'green';
            default:
                return 'red';
        }
    }

    const [isExpanded, toggleExpanded] = useState<boolean>(false);
    const defaultReportBodyHeight = 100;

    const reportBodyRef = useRef<HTMLDivElement>(null);

    const onView = () => {
        location.href = `http://192.168.49.2:30003/reports/${report.id}`
    }

    const onExpand = () => {
        const curr = reportBodyRef.current;

        if (curr) {
            if (curr.scrollHeight <= defaultReportBodyHeight) return;
            if (!isExpanded) curr.style.height = `${curr.scrollHeight}px`;
            else curr.style.height = `${defaultReportBodyHeight}px`;

            toggleExpanded(!isExpanded);
        }
    }

    const onCopy = (event: React.MouseEvent) => {
        navigator.clipboard.writeText(report.reportBody);
        event.stopPropagation();
    }

    return (
        <div className={styles.report}>
            <div className={styles.info}>
                <p className={styles.id}>
                    ID:
                    <span className={styles.value}>{report.id}</span>
                </p>
                <Label size={'s'} text={getLabelText()} theme={getLabelTheme()} type={'outline'} />
            </div>
            <div ref={reportBodyRef} className={styles.reportBody} onClick={onExpand}>
                <img
                    src={copyIcon.src}
                    className={styles.copyIcon}
                    alt="Copy text"
                    onClick={onCopy}
                />
                <p className={styles.text}>{report.reportBody}</p>
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