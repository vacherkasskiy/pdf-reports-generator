import NewReportInput from "@/components/ReportsPage/NewReportInput/NewReportInput";
import React from "react";
import {useAddReportMutation} from "@/api/services/ReportApi";
import {reportsApi} from "@/api/services/ReportsApi";
import {useAppDispatch} from "@/hooks/redux";

function NewReportContainer(): React.ReactElement {
    const dispatch = useAppDispatch();

    const [textareaValue, setTextareaValue] = React.useState<string | undefined>("");
    const [addReport, { isLoading, isSuccess, isError }] = useAddReportMutation();

    const handleOnAdd = async () => {
        await addReport(textareaValue).unwrap();
        dispatch(reportsApi.util.invalidateTags(['Reports']));
    }

    const handleOnChange = (value: string | undefined) => {
        setTextareaValue(value)
    }


    return (
        <>
            <NewReportInput
                text={textareaValue}
                onAdd={handleOnAdd}
                onChange={handleOnChange}
                isInProgress={isLoading}
                isSuccess={isSuccess}
                isError={isError}
            />
        </>
    )
}

export default NewReportContainer;