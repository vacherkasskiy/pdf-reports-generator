import NewReportInput from "@/components/ReportsPage/NewReportInput/NewReportInput";
import React from "react";
import {useCreateReportMutation} from "@/api/services/ReportApi";
import {reportsApi} from "@/api/services/ReportsApi";
import {useAppDispatch} from "@/hooks/redux";
import CreateReportRequest from "../../../api/requests/createReportRequest";

function NewReportContainer(): React.ReactElement {
    const dispatch = useAppDispatch();
    const [addReport, { isLoading, isSuccess, isError }] = useCreateReportMutation();

    const handleOnAdd = async (request: CreateReportRequest) => {
        await addReport(request).unwrap();
        dispatch(reportsApi.util.invalidateTags(['Reports']));
    }

    return (
        <>
            <NewReportInput
                onAdd={handleOnAdd}
                isInProgress={isLoading}
                isSuccess={isSuccess}
                isError={isError}
            />
        </>
    )
}

export default NewReportContainer;