import styles from "./ReportTask.module.scss";
import React from "react";
import {ReportModel} from "@/models";
import {Label, MyButton} from "@/ui";
import {theme} from "@/ui/utils";

interface ReportTaskProps {
    report: ReportModel
}

function ReportTask({report}: ReportTaskProps): React.ReactElement {
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
            case 0:
                return 'dark';
            case 1:
                return 'blue';
            case 2:
                return 'green';
            default:
                return 'red';
        }
    }

    const onClick = () => {
        location.href = `http://192.168.49.2:30003/reports/${report.id}`
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
            <div className={styles.buttons}>
                <MyButton
                    onClick={onClick}
                    disabled={report.link == null}
                    text={"View"}
                    theme={'blue'}
                />
                <MyButton text={"Generate again"} theme={'orange'} />
                <MyButton text={"Delete"} theme={'red'}/>
            </div>
        </div>
    )
}

export default ReportTask;