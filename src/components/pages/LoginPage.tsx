import React from "react";
import { RiTwitterFill } from "react-icons/ri";
import { signIn } from "next-auth/react";

const LoginPage = () => {
  return (
    <React.Fragment>
      <div className="flex flex-col items-start justify-center w-full px-6 pt-12 pb-24 lg:w-1/2 group">
        <p className="uppercase tracking-loose">Nukeitter</p>
        <div className="flex items-center justify-center group">
          <h1 className="flex-row my-4 text-3xl font-bold flx">
            Delete your Tweets on a schedule... or just once.
          </h1>
        </div>
        <p className="mb-4 leading-normal">
          You never know who&apos;s gonna read that radioactive Tweet you forgot
          you sent in 2017
        </p>
        <button
          className="gap-2 btn btn-outline btn-info"
          onClick={() => signIn("twitter")}
        >
          <RiTwitterFill className="w-6 h-6" />
          Sign in with Twitter
        </button>
      </div>
      <div className="w-full text-center lg:w-1/2 lg:py-6">
        <img
          src="/img/logo.svg"
          alt="Radioactive"
          className="w-3/5 mx-auto text-gray-900 fill-current"
        />
      </div>
    </React.Fragment>
  );
};

export default LoginPage;
