import ReportsPage from "@/components/ReportsPage/ReportsPage/ReportsPage";
import {useFetchReportsQuery} from "@/api/services/ReportsApi";

function ReportsPageContainer() {
    const {data: reports} =  useFetchReportsQuery()

    return (
        <>
            {reports && <ReportsPage
                reports={reports.toSorted((a, b) => new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime())}
            />}
        </>
    )
}

export default ReportsPageContainer;