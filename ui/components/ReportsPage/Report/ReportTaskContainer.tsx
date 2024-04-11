import ReportTask from "@/components/ReportsPage/Report/ReportTask";
import {ReportModel, ReportStatus} from "@/models";
import {useDeleteReportMutation, useRegenerateReportMutation} from "@/api/services/ReportsApi";
import React, {useState} from "react";
import {theme} from "@/ui/utils";
import {beautifyReportBody} from "@/utils";

interface ReportTaskContainerProps {
    report: ReportModel;
}

function ReportTaskContainer({report}: ReportTaskContainerProps): React.ReactElement {
    const [regenerateReport, {}] = useRegenerateReportMutation();
    const [deleteReport, {}] = useDeleteReportMutation();

    const [isExpanded, setExpanded] = useState<boolean>(false);

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

    const onView = () => {
        location.href = `http://192.168.49.2:30003/reports/${report.id}`
    }

    const onCopy = (event: React.MouseEvent) => {
        navigator.clipboard.writeText(beautifyReportBody(report.reportBody));
        event.stopPropagation();
    }

    const toggleExpanded = () => {
        setExpanded(!isExpanded);
    }

    return (
        <ReportTask
            report={report}
            onRegenerate={() => regenerateReport(report.id)}
            onDelete={() => deleteReport(report.id)}
            onView={onView}
            onCopy={onCopy}
            isExpanded={isExpanded}
            onExpand={toggleExpanded}
            labelTheme={getLabelTheme()}
            labelText={getLabelText()}
        />
    )
}

export default ReportTaskContainer;