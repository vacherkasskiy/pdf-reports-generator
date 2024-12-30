import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import {ReportModel} from "@/models";
import config from '../../config.json';

export const reportsApi = createApi({
    reducerPath: 'reportsApi',
    baseQuery: fetchBaseQuery({baseUrl: config.apiReportsBaseUrl}),
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