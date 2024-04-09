import ReportsPage from "@/components/ReportsPage/ReportsPage/ReportsPage";
import {useFetchReportsQuery} from "@/api/ReportsApi";

function ReportsPageContainer() {
    const {
        data: reports,
        isLoading,
    } =  useFetchReportsQuery()

    return (
        <>
            {reports && <ReportsPage reports={reports}/>}
        </>
    )
}

export default ReportsPageContainer;