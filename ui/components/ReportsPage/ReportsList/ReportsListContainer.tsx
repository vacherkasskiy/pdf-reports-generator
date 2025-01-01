import ReportsList from "@/components/ReportsPage/ReportsList/ReportsList";
import {ReportModel, ReportStatus} from "@/models";
import {HubConnectionBuilder} from "@microsoft/signalr";
import React, {useEffect} from "react";
import config from "../../../config.json";

interface ReportsListContainerProps {
    reports: ReportModel[];
}

export function ReportsListContainer({reports}: ReportsListContainerProps) {
    const [reportsList, setReportsList] = React.useState<ReportModel[]>(reports);

    useEffect(() => {
        setReportsList(reports);
    }, [reports]);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(config.webSocketUrl)
            .withAutomaticReconnect()
            .build();

        connection.start()
            .then(() => {
                console.log("Connected to the hub");

                connection.on("ReceivePdfReportTaskStatus", (guid: string, newStatus: ReportStatus) => {
                    console.log(`Received update for report ${guid} with status ${newStatus}`);

                    setReportsList((prevReports) =>
                        prevReports.map((report) =>
                            report.id === guid ? { ...report, status: newStatus } : report
                        )
                    );
                });
            })
            .catch(err => console.error("Error while connecting to hub:", err));

        return () => {
            connection.stop().then(() => console.log("Disconnected from the hub"));
        };
    }, []);

    return <ReportsList reports={reportsList} />;
}