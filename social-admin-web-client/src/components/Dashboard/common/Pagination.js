import React, { useState } from "react";

const Pagination = ({page, setPage, pages, isLoading}) => {

    const handlePage = (page) => {
        if (isLoading) return;
        if (page >= 0 && page < pages) {
            setPage(page)
        }
    }

    const renderPages = () => {
        const previousPage = page - 1 > -1 ?
            (<span className={"page"} onClick={() => handlePage(page - 1)}>{page}</span>) :
            null
        const nextPage = page + 1 < pages ?
            (<span className={"page"} onClick={() => handlePage(page + 1)}>{page + 2}</span>) :
            null

        return (
            <>
                {previousPage}
                <span className={"page current"}>{page + 1}</span>
                {nextPage}
            </>
        )
    }

    return (
        <div className={"pagination"}>
            <div className={"pages"}>
                <span className={"previous-page"} onClick={() => handlePage(page - 1)}>
                    <img src={"/images/previous_arrow.png"} alt={""}/>
                </span>
                {renderPages()}
                <span className={"next-page"} onClick={() => handlePage(page + 1)}>
                    <img src={"/images/next_arrow.png"} alt={""}/>
                </span>
            </div>
        </div>
    )
}

export default Pagination