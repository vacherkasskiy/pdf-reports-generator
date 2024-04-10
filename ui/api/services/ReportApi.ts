import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const reportApi = createApi({
    reducerPath: 'reportApi',
    baseQuery: fetchBaseQuery({baseUrl: 'https://localhost:7091/api/v1/reports'}),
    tagTypes: ['Reports'],
    endpoints: (build) => ({
        addReport: build.mutation<void, string | undefined>({
            query: (reportBody: string | undefined) => ({
                url: `/`,
                method: 'POST',
                body: JSON.stringify(JSON.parse(reportBody ?? "")),
                headers: {
                    'Content-Type': 'application/json'
                },
            }),
            invalidatesTags: ['Reports'],
        }),
    }),
})

export const {
    useAddReportMutation,
} = reportApi