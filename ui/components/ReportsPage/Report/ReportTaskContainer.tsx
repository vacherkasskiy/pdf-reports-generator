import ReportTask from "@/components/ReportsPage/Report/ReportTask";
import {ReportModel, ReportStatus} from "@/models";
import {useDeleteReportMutation} from "@/api/services/ReportsApi";
import React, {useEffect, useState} from "react";
import {theme} from "@/ui/utils";
import {beautifyReportBody} from "@/utils";
import config from '../../../config.json';
import {HubConnectionBuilder} from "@microsoft/signalr";

interface ReportTaskContainerProps {
    report: ReportModel;
}

function ReportTaskContainer({report}: ReportTaskContainerProps): React.ReactElement {
    const [deleteReport, {}] = useDeleteReportMutation();
    const [isExpanded, setExpanded] = useState<boolean>(false);
    const [status, setStatus] = useState<ReportStatus>(report.status);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(config.webSocketUrl)
            .withAutomaticReconnect()
            .build();

        connection.start().then(() => {
            console.log("Connected to the hub");

            connection.on("ReceivePdfReportTaskStatus", (guid: string, newStatus: ReportStatus) => {
                console.log(guid, newStatus);
                setStatus(newStatus);
            });
        }).catch(err => console.log('Error while connecting to hub: ', err));

        return () => {
            connection.stop().then();
        };
    }, []);

    const getLabelText = (): string => {
        switch (status) {
            case ReportStatus.Waiting:
                return 'WAITING';
            case ReportStatus.InProgress:
                return 'IN PROGRESS';
            case ReportStatus.Ready:
                return 'READY';
            default:
                return 'ERROR';
        }
    }

    const getLabelTheme = (): theme => {
        switch (status) {
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

    const onDownload = () => {
        location.href = `${config.apiReportsBaseUrl}/download/${report.id}`
    }

    const onCopy = (event: React.MouseEvent) => {
        navigator.clipboard.writeText(beautifyReportBody(report.reportBody)); // todo add then. notify user.
        event.stopPropagation();
    }

    const toggleExpanded = () => {
        setExpanded(!isExpanded);
    }

    return (
        <ReportTask
            report={report}
            onDelete={() => deleteReport(report.id)}
            onView={onDownload}
            onCopy={onCopy}
            isExpanded={isExpanded}
            onExpand={toggleExpanded}
            labelTheme={getLabelTheme()}
            labelText={getLabelText()}
        />
    )
}

export default ReportTaskContainer;