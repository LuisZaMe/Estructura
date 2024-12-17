import React from "react";

const BrandPanel = () => {
    return (
        <div className={"brand-panel"}>
            <img className={"logo-puzzle-blue"} src={"/images/logo_puzzle_blue.png"} alt={"Logo Puzzle Blue"}/>
            <img className={"logo-main"} src={"/images/logo.png"} alt={"Logo"}/>
            <img className={"logo-puzzle-green"} src={"/images/logo_puzzle_green.png"} alt={"Logo Puzzle Green"}/>
        </div>
    )
}

export default BrandPanel;