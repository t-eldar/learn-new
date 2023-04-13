declare module "models" {
  type User = {
    id: string;
    name: string;
    surname: string;
    email: string;
    scores?: TestScore[];
    courses?: Course[];
  };
  type TestScore = {
    id: number;
    testingDate: Date;
    score: number;

    userId: string;
    user?: User;

    testId: number;
    test?: Test;

    questionScores?: QuestionScore[];
  };
  type QuestionScore = {
    id: number;
    isCorrect: boolean;
    userAnswerText: string;

    testScoreId: number;
    testScore?: TestScore;

    userId: string;
    user?: User;

    questionId: number;
    question?: Question;
  };
  type Course = {
    id: number;
    coverImageUrl: string;
    name: string;
    description: string;
    dateCreated: Date;

    userId: string;
    user?: User;

    lessons?: Lesson[];
  };
  type Lesson = {
    id: number;
    title: string;
    content: string;
    isHidden: boolean;

    courseId: number;
    course?: Course;

    userId: string;
    user?: User;

    tests?: Test[];
  };
  type Answer = {
    id: number;
    text: string;
    isCorrect: boolean;

    questionId: number;
    question?: Question;

    userId: string;
    user?: User;
  };
  type Question = {
    id: number;
    content: string;
    areAnswersChoicable: boolean;

    testId: number;
    test?: Test;

    userId: string;
    user?: User;

    answers?: Answer[];
  };
  type Test = {
    id: number;
    title: string;

    lessonId: number;
    lesson?: Lesson;

    userId: string;
    user?: User;

    questions?: Question[];
  };

  type SignInRequest = {
    email: string;
    password: string;
    rememberMe: boolean;
  };

  type SignUpRequest = {
    name: string;
    surname: string;
    email: string;
    password: string;
  };

  type CheckTestRequest = {
    testId: number;
    answerRequests: UserAnswerRequest[];
  };

  type UserAnswerRequest = {
    userId: string;
    questionId: number;
    answerText: string;
  };
  type CreateAnswerRequest = Omit<Answer, "user" | "question" | "id">;
  type UpdateAnswerRequest = Pick<Answer, "id"> &
    Partial<Omit<Answer, "user" | "userId" | "question" | "id">>;

  type CreateCourseRequest = Omit<
    Course,
    "user" | "lessons" | "id" | "dateCreated"
  >;
  type UpdateCourseRequest = Pick<Course, "id"> &
    Partial<Pick<Course, "name" | "description" | "coverImageUrl">>;

  type CreateLessonRequest = Omit<Lesson, "course" | "user" | "tests" | "id">;
  type UpdateLessonRequest = Pick<Lesson, "id"> &
    Partial<Pick<Lesson, "title" | "content" | "courseId" | "isHidden">>;

  type CreateQuestionRequest = Omit<
    Question,
    "test" | "user" | "answers" | "id"
  >;
  type UpdateQuestionRequest = Pick<Question, "id"> &
    Partial<Pick<Question, "content" | "testId" | "areAnswersChoicable">>;

  type CreateEmptyTestRequest = Omit<
    Test,
    "lesson" | "user" | "questions" | "id"
  >;
  type EmptyAnswer = Omit<CreateAnswerRequest, "questionId" | "userId">;
  type EmptyQuestion = Omit<CreateQuestionRequest, "testId" | "userId"> & {
    answers: EmptyAnswer[];
  };
  type CreateFullTestRequest = Omit<
    Test,
    "lesson" | "user" | "questions" | "id"
  > & {
    questions: EmptyQuestion[];
  };
  type UpdateTestRequest = Pick<Test, "id"> &
    Partial<Pick<Test, "title" | "lessonId">>;
}
