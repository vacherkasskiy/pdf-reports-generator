import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import {ReportModel} from "@/models";

export const reportsApi = createApi({
    reducerPath: 'reportsApi',
    baseQuery: fetchBaseQuery({baseUrl: 'https://localhost:7090/app/v1/reports'}),
    tagTypes: ['Reports'],
    endpoints: (build) => ({
        fetchReports: build.query<ReportModel[], void>({
            query: () => ({
                url: '/',
                method: 'GET'
            }),
            providesTags: ['Reports'],
        }),
        regenerateReport: build.mutation<void, string>({
            query: (id: string) => ({
                url: `/regenerate/${id}`,
                method: 'PATCH',
            }),
            invalidatesTags: ['Reports'],
        }),
        deleteReport: build.mutation<void, string>({
            query: (id: string) => ({
                url: `/delete/${id}`,
                method: 'DELETE'
            }),
            invalidatesTags: ['Reports'],
        }),
    }),
})

export const {
    useFetchReportsQuery,
    useRegenerateReportMutation,
    useDeleteReportMutation,
} = reportsApi