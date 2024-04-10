import ReportsPage from "@/components/ReportsPage/ReportsPage/ReportsPage";
import {
    useDeleteReportMutation,
    useFetchReportsQuery,
    useRegenerateReportMutation
} from "@/api/services/ReportsApi";

function ReportsPageContainer() {
    const [regenerateReport, {}] = useRegenerateReportMutation();
    const [deleteReport, {}] = useDeleteReportMutation();

    const {
        data: reports,
        isLoading,
    } =  useFetchReportsQuery()

    return (
        <>
            {reports && <ReportsPage
                reports={reports.toSorted((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime())}
                onRegenerate={regenerateReport}
                onDelete={deleteReport}
            />}
        </>
    )
}

export default ReportsPageContainer;